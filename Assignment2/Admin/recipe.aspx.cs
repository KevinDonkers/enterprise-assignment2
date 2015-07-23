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
            //try
            //{
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
                                    ing.ingredient_id, 
                                    ing.ingredient_name, 
                                    mes.measurement, 
                                    mes.unit 
                                };

                    //populate the form
                    txtRecipeName.Text = r.FirstOrDefault().recipe_name;
                    grdIngredients.DataSource = r.ToList();
                    grdIngredients.DataBind();

                }
            /*}
            catch (Exception e)
            {
                //Response.Redirect("~/error.aspx");
            }*/
        }

        protected void btnAddIngredient_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void grdIngredients_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}