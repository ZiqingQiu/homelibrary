﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookDetails : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            BindDetails();
        }
    }

    private void BindDetails()
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataReader reader;

        string connectionString = ConfigurationManager.ConnectionStrings["Homelibrary"].ConnectionString;
        conn = new SqlConnection(connectionString);
        comm = new SqlCommand("SELECT * FROM books WHERE ISBN=@ISBN", conn);

        //@ISBN
        comm.Parameters.Add("ISBN", System.Data.SqlDbType.NVarChar, 13);
        comm.Parameters["ISBN"].Value = Session["CurrrentViewBook"] as string;

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            bookDetailsView.DataSource = reader;
            bookDetailsView.DataKeyNames = new string[] { "ISBN" };
            bookDetailsView.DataBind();
            reader.Close();
        }
        finally
        {
            conn.Close();
        }
    }

    protected void bookDetailsView_ItemDeleting(object sender, DetailsViewDeleteEventArgs e)
    {
        string ISBN = bookDetailsView.DataKey.Value as string;

        SqlConnection conn;
        SqlCommand comm;
        SqlDataReader reader;

        string connectionString = ConfigurationManager.ConnectionStrings["Homelibrary"].ConnectionString;
        conn = new SqlConnection(connectionString);
        comm = new SqlCommand("DELETE FROM books WHERE ISBN=@ISBN", conn);

        //@ISBN
        comm.Parameters.Add("ISBN", System.Data.SqlDbType.NVarChar, 13);
        comm.Parameters["ISBN"].Value = ISBN;

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            bookDetailsView.DataSource = reader;
            bookDetailsView.DataKeyNames = new string[] { "ISBN" };
            bookDetailsView.DataBind();
            reader.Close();
        }
        finally
        {
            conn.Close();
        }
    }



    protected void bookDetailsView_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        bookDetailsView.ChangeMode(e.NewMode);
        BindDetails();
    }

    protected void bookDetailsView_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        int oldISBN = (int)bookDetailsView.DataKey.Value;
        //get user input from each edit text box
        string newTitle = ((TextBox)bookDetailsView.FindControl("editTitleTxtBox")).Text;
        string newAuthor = ((TextBox)bookDetailsView.FindControl("editAuthorNameTxtBox")).Text;
        string newISBN = ((TextBox)bookDetailsView.FindControl("editISBNTxtBox")).Text;
        string newGenre = ((TextBox)bookDetailsView.FindControl("editGenreTxtBox")).Text;
        string newTotalPage = ((TextBox)bookDetailsView.FindControl("editGenreTxtBox")).Text;
        string newIsLanded = ((TextBox)bookDetailsView.FindControl("editIsLandedTxtBox")).Text;
        string newFriendName = ((TextBox)bookDetailsView.FindControl("editFriendNameTxtBox")).Text;
        string newComments = ((TextBox)bookDetailsView.FindControl("editCommentsTxtBox")).Text;


        //update database
        SqlConnection conn;
        SqlCommand comm;

        string connectionString = ConfigurationManager.ConnectionStrings["Homelibrary"].ConnectionString;
        conn = new SqlConnection(connectionString);
        comm = new SqlCommand("UPDATE books SET " +
            "ISBN=@ISBN, Title=@Title, Author=@Author, Genre=@Genre, Pages=@Pages, Landed=@Landed, Friend=@Friend, Comments=@Comments)" +
            "WHERE ISBN=@oldISBN", conn);
        //@ISBN
        comm.Parameters.Add("ISBN", System.Data.SqlDbType.NVarChar, 13);
        comm.Parameters["ISBN"].Value = newISBN;
        comm.Parameters.Add("oldISBN", System.Data.SqlDbType.NVarChar, 13);
        comm.Parameters["oldISBN"].Value = oldISBN;
        //@Title
        comm.Parameters.Add("Title", System.Data.SqlDbType.NVarChar, 20);
        comm.Parameters["Title"].Value = bi1.BookName;
        //@Author
        comm.Parameters.Add("Author", System.Data.SqlDbType.NVarChar, 20);
        comm.Parameters["Author"].Value = bi1.AuthorName;
        //@Genre
        comm.Parameters.Add("Genre", System.Data.SqlDbType.NVarChar, 10);
        if (String.IsNullOrEmpty(lbxGenre.Text))
        {
            comm.Parameters["Genre"].Value = DBNull.Value;
        }
        else
        {
            comm.Parameters["Genre"].Value = lbxGenre.Text;
        }

        //@Pages
        comm.Parameters.Add("Pages", System.Data.SqlDbType.Int);
        comm.Parameters["Pages"].Value = txtNumOfPages.Text;
        //@Landed
        comm.Parameters.Add("Landed", System.Data.SqlDbType.Char, 1);
        if (rdoLanded.Checked)
        {
            comm.Parameters["Landed"].Value = 'Y';
        }
        else
        {
            comm.Parameters["Landed"].Value = 'N';
        }
        //@Friend
        comm.Parameters.Add("Friend", System.Data.SqlDbType.NVarChar, 20);
        if (String.IsNullOrEmpty(txtLandFriName.Text))
        {
            comm.Parameters["Friend"].Value = DBNull.Value;
        }
        else
        {
            comm.Parameters["Friend"].Value = txtLandFriName.Text;
        }
        //@Comments
        comm.Parameters.Add("Comments", System.Data.SqlDbType.NVarChar, 50);
        if (String.IsNullOrEmpty(txtComments.Text))
        {
            comm.Parameters["Comments"].Value = DBNull.Value;
        }
        else
        {
            comm.Parameters["Comments"].Value = txtComments.Text;
        }

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
            Response.Redirect("AddBooks.aspx");
        }
        catch
        {
            Response.Write("Error adding new book.");
        }
        finally
        {
            conn.Close();
        }


    }
}