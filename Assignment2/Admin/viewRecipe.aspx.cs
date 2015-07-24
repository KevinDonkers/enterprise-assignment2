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
    public partial class viewRecipe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check for the id in url
            if (!IsPostBack)
            {

                if (!String.IsNullOrEmpty(Request.QueryString["recipe_id"]))
                {
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
                RecipeTitle.Text = r.FirstOrDefault().recipe_name;
                RecipeDirections.Text = r.FirstOrDefault().directions;

                grdViewIngredients.DataSource = r.ToList();
                grdViewIngredients.DataBind();
            }
            }
            catch (Exception e)
            {
                Response.Redirect("~/error.aspx");
            }
        }
    }
}