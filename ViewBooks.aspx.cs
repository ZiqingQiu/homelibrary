﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewBooks : System.Web.UI.Page
{

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //add session based theme
        if (Session["CurrrentTheme"] != null)
        {
            this.Theme = Session["CurrrentTheme"] as string;
        }
        else
        {
            this.Theme = "Light";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //use gridview to retreive data from database
        if (!IsPostBack)
        {
            //replace to datasouce solution
            //BindGrid();
        }
    }

    //private void BindGrid()
    //{
    //    SqlConnection conn;
    //    SqlCommand comm;
    //    SqlDataReader reader;

    //    string connectionString = ConfigurationManager.ConnectionStrings["Homelibrary"].ConnectionString;
    //    conn = new SqlConnection(connectionString);
    //    comm = new SqlCommand("SELECT Title, Author, ISBN FROM books", conn);

    //    try
    //    {
    //        conn.Open();
    //        reader = comm.ExecuteReader();
    //        //After successfully tested datalist, replaced by gridview
    //        //DatalistTest.DataSource = reader;
    //        //DatalistTest.DataBind();
    //        booksGridView.DataSource = reader;
    //        booksGridView.DataBind();
    //        reader.Close();
    //    }
    //    finally
    //    {
    //        conn.Close();
    //    }
    //}

    //After successfully tested datalist, replaced by gridview
    //protected void booksDataList_ItemCommand(object source, DataListCommandEventArgs e)
    //{
    //    if (e.CommandName == "MoreDetailsPlease")
    //    {
    //        Literal li;
    //        li = e.Item.FindControl("extraDetailsLiteral") as Literal;
    //        li.Text = "Command Test: <strong>" + e.CommandArgument +  "</strong><br />";
    //    }
    //}

    protected void booksGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "MoreDetailsPlease")
        {
            string isbn = e.CommandArgument as string;
            Response.Redirect("BookDetails.aspx?isbn=" + isbn);          
        }
    }
}