<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="recipe.aspx.cs" Inherits="Assignment2.Admin.recipe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Recipe</h1>

    <div class="form-group">
        <label for="txtRecipeName" class="col-sm-3">Recipe Name:</label>
        <asp:TextBox ID="txtRecipeName" runat="server" required="true" MaxLength="50" />
    </div>

    <asp:Panel ID="pnlIngredients" runat="server">
        <h2>Ingredients</h2>
        <asp:GridView ID="grdIngredients" runat="server" CssClass="table table-striped table-hover"
            AutoGenerateColumns="false" DataKeyNames="ingredient_id"
            OnRowDeleting="grdIngredients_RowDeleting" GridLines="none" >
            <Columns>
                <asp:BoundField DataField="ingredient_name" HeaderText="Ingredient" />
                <asp:BoundField DataField="measurement" HeaderText="Amount" />
                <asp:BoundField DataField="unit" HeaderText="Unit" />
                <asp:HyperLinkField HeaderText="Edit" NavigateUrl="~/recipe.aspx" Text="Edit"
                DataNavigateUrlFields="ingredient_id, recipe_id" DataNavigateUrlFormatString="ingredient.aspx?ingredient_id={0}&recipe_id={1}" />
                <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <div class="col-sm-offset-3">
        <asp:Button ID="btnAddIngredient" runat="server" Text="Save" CssClass="btn btn-primary" 
        OnClick="btnAddIngredient_Click"/>
    </div>

    <div class="col-sm-offset-3">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" 
        OnClick="btnSave_Click"/>
    </div>

</asp:Content>
