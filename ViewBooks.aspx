﻿<%-- Ziqing Qiu 300919236 --%>
<%-- COMP229 HomeLibrary Assignment --%>
<%-- ViewBooks.asax 
     1. display all the books this library have--%>
<%@ Page Title="View Books" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewBooks.aspx.cs" Inherits="ViewBooks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagecontent" Runat="Server">
    <div id="ViewBookContentPosition">
        <asp:SqlDataSource ID="viewBooksDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Homelibrary %>" SelectCommand="SELECT [ISBN], [Title], [Author] FROM [Books]"></asp:SqlDataSource>
        <asp:GridView ID="booksGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="booksGridView_RowCommand" PageSize="5" EmptyDataText="Current no book in library!" AllowPaging="True" DataSourceID="viewBooksDataSource">
            <Columns>
                <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                <asp:TemplateField HeaderText="Book Title">
                    <ItemTemplate>      
                        <asp:LinkButton ID="detailsButton" runat="server" Text=<%# Eval("Title") %> 
                            CommandName="MoreDetailsPlease" CommandArgument=<%# Eval("ISBN") %>></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Author" HeaderText="Author Name" />                     
            </Columns>
        </asp:GridView>

<%--    <asp:DataList ID="DatalistTest" runat="server" OnItemCommand="booksDataList_ItemCommand" >
            <ItemTemplate>
                ISBN: <%# Eval("ISBN") %>
                <br />
                <asp:Literal ID="extraDetailsLiteral" runat="server" EnableViewState="False" />
                    Book Title: <asp:LinkButton ID="detailsButton" runat="server" Text=<%# Eval("Name") %>
                    CommandName ="MoreDetailsPlease" CommandArgument=<%# Eval("ISBN") %>/>
                <br />                
            </ItemTemplate>
        </asp:DataList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>--%>
    </div>
</asp:Content>

