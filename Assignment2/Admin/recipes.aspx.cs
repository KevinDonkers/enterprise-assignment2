using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference our entity framework models
using Assignment2.Models;
using System.Web.ModelBinding;

using System.Linq.Dynamic;

namespace Assignment2.Admin
{
    public partial class recipes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //fill the grid
            if (!IsPostBack)
            {
                Session["SortDirection"] = "ASC";
                Session["SortColumn"] = "recipe_id";
                GetRecipes();
            }
        }

        protected void GetRecipes()
        {
            try
            { 
                //connect using our connection string from web.config and EF context class
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {

                    //use link to query the Recipes model
                    var recipes = from r in conn.Recipes
                                  select r;

                    //append the current direction to the sort column
                    String sort = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();
                    grdRecipes.DataSource = recipes.AsQueryable().OrderBy(sort).ToList();
                    grdRecipes.DataBind();
                }
            }
            catch (Exception e)
            {
                Response.Redirect("~/error.aspx");
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the page size and refresh
            grdRecipes.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetRecipes();
        }

        protected void grdRecipes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            { 
                //connect
                using (DefaultConnectionEF conn = new DefaultConnectionEF())
                {
                    //get the selected recipe_id
                    Int32 Recipe_id = Convert.ToInt32(grdRecipes.DataKeys[e.RowIndex].Values["recipe_id"]);

                    var recipe = (from r in conn.Recipes
                                  where r.recipe_id == Recipe_id
                                  select r).FirstOrDefault();

                    var measure = (from mes in conn.Measurements
                                   where mes.recipe_id == Recipe_id
                                   select mes);

                    //delete
                    conn.Measurements.RemoveRange(measure);
                    conn.Recipes.Remove(recipe);
                    conn.SaveChanges();

                    //update the grid
                    GetRecipes();

                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/error.aspx");
            }
        }

        protected void grdRecipes_Sorting(object sender, GridViewSortEventArgs e)
        {
            //set the global sort cloumn to the column clicked on by the user
            Session["SortColumn"] = e.SortExpression;
            GetRecipes();

            //toggle the direction
            if (Session["SortDirection"] == "ASC")
            {
                Session["SortDirection"] = "DESC";
            }
            else
            {
                Session["SortDirection"] = "ASC";
            }
        }

        protected void grdRecipes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i < grdRecipes.Columns.Count; i++)
                    {
                        if (grdRecipes.Columns[i].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "DESC")
                            {
                                SortImage.ImageUrl = "/images/desc.jpg";
                                SortImage.AlternateText = "Sort Decending";
                            }
                            else
                            {
                                SortImage.ImageUrl = "/images/asc.jpg";
                                SortImage.AlternateText = "Sort Ascending";
                            }

                            e.Row.Cells[i].Controls.Add(SortImage);

                        }
                    }
                }
            }
        }

        protected void grdRecipes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the page index and refresh the grid
            grdRecipes.PageIndex = e.NewPageIndex;
            GetRecipes();
        }
    }
}