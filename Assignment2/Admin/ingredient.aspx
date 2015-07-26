<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ingredient.aspx.cs" Inherits="Assignment2.Admin.ingredient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Ingredient</h1>

    <div class="form-group">
        <label for="txtIngredientName" class="col-sm-3">Name:</label>
        <asp:TextBox ID="txtIngredientName" runat="server" required="true" MaxLength="50" />
    </div>
    <div class="form-group">
        <label for="txtMeasurement" class="col-sm-3">Amount:</label>
        <asp:TextBox ID="txtMeasurement" runat="server" required="true" MaxLength="5" />
    </div>
    <div class="form-group">
        <label for="ddlUnit" class="col-sm-3">Unit</label>
        <asp:DropDownList ID="ddlUnit" runat="server" required="true">
            <asp:ListItem Value="Cups" Text="Cups" />
            <asp:ListItem Value="Cup" Text="Cup" />
            <asp:ListItem Value="tbsp." Text="tbsp." />
            <asp:ListItem Value="tsp." Text="tsp." />
            <asp:ListItem Value="oz." Text="oz." />
            <asp:ListItem Value="grams" Text="grams" />
            <asp:ListItem Value="lbs." Text="lbs." />
            <asp:ListItem Value="lb" Text="lb" />
            <asp:ListItem Value="kg" Text="kg" />
        </asp:DropDownList>
    </div>
    <div class="col-sm-offset-3">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" 
        OnClick="btnSave_Click"/>
    </div>

</asp:Content>
