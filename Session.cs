// Set
Session["test"] = lblTest.Text;

// get
if (Session["test"] != null)
{
    lblTest.Text = "Hello" + Session["test"];
}
else
{
    // do something 
}


//set DataSet
Session["DataSet"] = myDataSet;

//get DataSet
if (Session["DataSet"] != null)
{
    DataSet myDs = (DataSet)Session["DataSet"];
}
else
{
    // do something 
}


<configuration>
<system.web>
<sessionstate mode="Off|Inproc|StateServer|SQLServer|Custom">
</sessionstate></system.web>
</configuration>