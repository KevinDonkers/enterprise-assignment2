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
    public partial class recipe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check for the id in url
            if (!IsPostBack)
            {

                if (!String.IsNullOrEmpty(Request.QueryString["recipe_id"]))
                {
                    //we have a parameter populate the form
                    GetRecipe();
                }
            }
        }

        protected void GetRecipe()
        {
            try
            {
                //connect
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    //get the recipe id
                    Int32 RecipeID = Convert.ToInt32(Request.QueryString["recipe_id"]);

                    //get recipe info
                    var r = from rec in conn.Recipes
                               join mes in conn.Measurements on rec.recipe_id equals mes.recipe_id
                               join ing in conn.Ingredients on mes.ingredient_id equals ing.ingredient_id
                               where rec.recipe_id == RecipeID
                                select new 
                                { 
                                    rec.recipe_id,
                                    rec.recipe_name,
                                    rec.directions,
                                    ing.ingredient_id, 
                                    ing.ingredient_name, 
                                    mes.measurement, 
                                    mes.unit 
                                };

                    //populate the form
                    txtRecipeName.Text = r.FirstOrDefault().recipe_name;
                    txtRecipeDirections.Text = r.FirstOrDefault().directions;

                    grdIngredients.DataSource = r.ToList();
                    grdIngredients.DataBind();

                    pnlIngredients.Visible = true;
                }
            }
            catch (Exception e)
            {
                Response.Redirect("~/error.aspx");
            }
        }

        protected void btnAddIngredient_Click(object sender, EventArgs e)
        {
            Int32 RecipeID;


            try 
            {

                if (!String.IsNullOrEmpty(Request.QueryString["recipe_id"]))
                {
                    //get the recipe id
                    RecipeID = Convert.ToInt32(Request.QueryString["recipe_id"]);                
                }
                else
                {
                    //connect
                    using (DefaultConnectionEF conn = new DefaultConnectionEF())
                    {
                        //instantiate a new recipe object in memory
                        Recipe r = new Recipe();

                        //fill the properties of our object from the form inputs
                        r.recipe_name = txtRecipeName.Text;
                        r.directions = txtRecipeDirections.Text;
                    
                    
                        //add the new object and save changes
                        conn.Recipes.Add(r);
                        conn.SaveChanges();

                        //get recipe info
                        var Recipe = (from rec in conn.Recipes
                                        where rec.recipe_name == r.recipe_name && rec.directions == r.directions
                                        select new { rec.recipe_id}).FirstOrDefault();

                        RecipeID = Recipe.recipe_id;
                    }
                }

                Response.Redirect("~/Admin/ingredient.aspx?recipe_id=" + RecipeID);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            { 
                if (String.IsNullOrEmpty(Request.QueryString["recipe_id"]))
                {
                    lblIngredientMessage.Text = "At Least One Ingredent Must be Added";
                    lblIngredientMessage.Visible = true;
                } 
                else
                {
                    using (DefaultConnectionEF conn = new DefaultConnectionEF())
                    {
                        Recipe r = new Recipe();
                        Int32 RecipeID = Convert.ToInt32(Request.QueryString["recipe_id"]);

                        r = (from rec in conn.Recipes
                             where rec.recipe_id == RecipeID
                             select rec).FirstOrDefault();

                        //fill the properties of our object from the form inputs
                        r.recipe_name = txtRecipeName.Text;
                        r.directions = txtRecipeDirections.Text;

                        conn.SaveChanges();

                        lblIngredientMessage.Visible = false;
                        Response.Redirect("/Admin/recipes.aspx");
                    }
                }
            }
            catch (Exception exception)
            {
                lblError.Text = exception.Message;
            }
        }

        protected void grdIngredients_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //get the selected ingredient id
            Int32 IngredientID = Convert.ToInt32(grdIngredients.DataKeys[e.RowIndex].Values["ingredient_id"]);
            //get the recipe id
            Int32 RecipeID = Convert.ToInt32(Request.QueryString["recipe_id"]);     

            try
            {
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {

                    Measurement objM = (from mes in conn.Measurements
                                       where mes.recipe_id == RecipeID && mes.ingredient_id == IngredientID
                                       select mes).FirstOrDefault();

                    conn.Measurements.Remove(objM);
                    conn.SaveChanges();

                    GetRecipe();

                }
            }
            catch (Exception exc)
            {
                Response.Redirect("~/error.aspx");
            }
        }
    }
}