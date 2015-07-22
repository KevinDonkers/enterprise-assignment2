<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="recipe.aspx.cs" Inherits="Assignment2.Admin.recipe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Recipe</h1>

    <div class="form-group">
        <label for="txtTitle" class="col-sm-3">Recipe Name:</label>
        <asp:TextBox ID="txtTitle" runat="server" required="true" MaxLength="50" />
    </div>

    <asp:Panel ID="Ingredients" runat="server">
        <div  class="form-group">
            <label for="txtIngredientName1" class="col-sm-2">Ingredient 1 Name:</label>
            <asp:TextBox ID="txtIngredientName1" runat="server" required="true" MaxLength="50" />
            <label for="txtIngredientAmount1" class="col-sm-2">Ingredient 1 Amount:</label>
            <asp:TextBox ID="txtIngredientAmount1" runat="server" required="true" MaxLength="50" />
            <label for="txtIngredientUnit1" class="col-sm-2">Ingredient 1 Unit:</label>
            <asp:DropDownList ID="ddlUnits" runat="server" required="true" />
        </div>
    </asp:Panel>

    <div class="form-group">
        <asp:Button ID="btnAddIngredient" runat="server" Text="Add Another Ingredient" CssClass="btn btn-default" 
                OnClick="btnAddIngredient_Click" />
    </div>

    <div class="form-group">
        <label for="txtDepartment" class="col-sm-3">Department</label>
        
    </div>
    <div class="col-sm-offset-3">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" 
        OnClick="btnSave_Click"/>
    </div>

</asp:Content>
