<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="recipes.aspx.cs" Inherits="Assignment2.Admin.recipes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Recipes</h1>
    <a href="recipe.aspx">Add A Recipe</a>
    <div>
        <label for="ddlPageSize">Records Per Page:</label>
        <asp:DropDownList ID="ddlPageSize" runat="server" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Value="3" Text="3" />
            <asp:ListItem Value="5" Text="5" />
            <asp:ListItem Value="10" Text="10" />
        </asp:DropDownList>
    </div>
    
    <asp:GridView ID="grdRecipes" runat="server" CssClass="table table-hover"
        AutoGenerateColumns="false" OnRowDeleting="grdRecipes_RowDeleting" 
        DataKeyNames="recipe_id" AllowPaging="true" OnPageIndexChanging="grdRecipes_PageIndexChanging" PageSize="3"
        OnSorting="grdRecipes_Sorting" AllowSorting="true" OnRowDataBound="grdRecipes_RowDataBound" GridLines="None">
        <Columns>
            <asp:HyperLinkField DataTextField="recipe_name" SortExpression="recipe_name" HeaderText="Recipe Name"
                NavigateUrl="~/Admin/viewRecipe.aspx" DataNavigateUrlFormatString="viewRecipe.aspx?recipe_id={0}"
                DataNavigateUrlFields="recipe_id" />
            <asp:HyperLinkField HeaderText="Edit" NavigateUrl="~/recipe.aspx" Text="Edit"
                DataNavigateUrlFields="recipe_id" DataNavigateUrlFormatString="recipe.aspx?recipe_id={0}" />
            <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
