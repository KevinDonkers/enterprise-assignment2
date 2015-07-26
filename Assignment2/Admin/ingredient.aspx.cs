using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Assignment2.Models;
using System.Web.ModelBinding;

namespace Assignment2.Admin
{
    public partial class ingredient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check for the id in url
            if (!IsPostBack)
            {

                if (!String.IsNullOrEmpty(Request.QueryString["ingredient_id"]))
                {
                    //we have a parameter populate the form
                    GetIngredient();
                }
            }
        }

        protected void GetIngredient()
        {
            try
            {
            //connect
            using (DefaultConnectionEF conn = new DefaultConnectionEF())
            {
                //get the ingredient id and the recipe_id
                Int32 IngredientID = Convert.ToInt32(Request.QueryString["ingredient_id"]);
                Int32 RecipeID = Convert.ToInt32(Request.QueryString["recipe_id"]);

                //get recipe info
                var i = (from rec in conn.Recipes
                         join mes in conn.Measurements on rec.recipe_id equals mes.recipe_id
                         join ing in conn.Ingredients on mes.ingredient_id equals ing.ingredient_id
                         where (rec.recipe_id == RecipeID) && (ing.ingredient_id == IngredientID)
                         select new { ing.ingredient_name, mes.measurement, mes.unit }).FirstOrDefault();

                //populate the form
                txtIngredientName.Text = i.ingredient_name;
                txtMeasurement.Text = i.measurement;
                
                //go through the unit ddl and select the matching unit
                foreach (ListItem item in ddlUnit.Items)
                {
                    if (item.Text == i.unit)
                    {
                        ddlUnit.SelectedValue = item.Value;
                    }
                }

            }
            }
            catch (Exception e)
            {
                Response.Redirect("~/error.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (DefaultConnectionEF conn = new DefaultConnectionEF())
            {
                //instantiate a new student object in memory
                Ingredient i = new Ingredient();
                Measurement m = new Measurement();
                Int32 RecipeID = Convert.ToInt32(Request.QueryString["recipe_id"]);

                //decide if updating or adding, then save
                if (!String.IsNullOrEmpty(Request.QueryString["ingredient_id"]))
                {
                    Int32 IngredientID = Convert.ToInt32(Request.QueryString["ingredient_id"]);

                    i = (from ing in conn.Ingredients
                            join mes in conn.Measurements on ing.ingredient_id equals mes.ingredient_id
                            where ing.ingredient_id == IngredientID
                            select ing).FirstOrDefault();

                    //fill the properties of our object from the form inputs
                    i.Measurements.FirstOrDefault().measurement = txtMeasurement.Text;
                    i.Measurements.FirstOrDefault().unit = ddlUnit.SelectedValue;
                }

                i.ingredient_name = txtIngredientName.Text;

                if (String.IsNullOrEmpty(Request.QueryString["ingredient_id"]))
                {
                    m.measurement = txtMeasurement.Text;
                    m.unit = ddlUnit.SelectedValue;
                    m.ingredient_id = i.ingredient_id;
                    m.recipe_id = RecipeID;

                    
                    conn.Ingredients.Add(i);
                    conn.Measurements.Add(m);
                }
                conn.SaveChanges();

                //redirect to updated departments page
                Response.Redirect("~/Admin/recipe.aspx?recipe_id=" + RecipeID);
            }
        }
    }
}