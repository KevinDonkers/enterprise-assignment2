<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="viewRecipe.aspx.cs" Inherits="Assignment2.Admin.viewRecipe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2><asp:Label ID="RecipeTitle" runat="server"></asp:Label></h2>

    <h2>Ingredients</h2>
        <asp:GridView ID="grdViewIngredients" runat="server" CssClass="table table-hover"
            AutoGenerateColumns="false" DataKeyNames="ingredient_id" GridLines="none" ShowHeader="false" >
            <Columns>
                <asp:BoundField DataField="measurement" HeaderText="Amount" />
                <asp:BoundField DataField="unit" HeaderText="Unit" />
                <asp:BoundField DataField="ingredient_name" HeaderText="Ingredient" />
            </Columns>
        </asp:GridView>

    <asp:label ID="RecipeDirections" runat="server"></asp:label>
</asp:Content>
