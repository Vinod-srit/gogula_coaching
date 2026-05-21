using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

string connectionString = "server=localhost;database=coachingcenter;user=root;password=Vinod@2002;";

app.MapPost("/saveStudent", async (HttpContext context) =>
{
    var form = await context.Request.ReadFormAsync();

    string fname = form["fname"].ToString();
    string lname = form["lname"].ToString();
    string year_of_passout = form["year_of_passout"].ToString();
    string course = form["course"].ToString();
    string mobile_number = form["mobile_number"].ToString();
    string graduation = form["graduation"].ToString();
    string address = form["address"].ToString();
    string reference = form["reference"].ToString();

    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = @"insert into students
        (fname,lname,year_of_passout,mobile_number,graduation,address,reference)
        values
        (@fname,@lname,@year_of_passout,@mobile_number,@graduation,@address,@reference)";

        MySqlCommand cmd = new MySqlCommand(query, con);

        cmd.Parameters.AddWithValue("@fname", fname);
        cmd.Parameters.AddWithValue("@lname", lname);
        cmd.Parameters.AddWithValue("@year_of_passout", year_of_passout);
        cmd.Parameters.AddWithValue("@course",course);
        cmd.Parameters.AddWithValue("@mobile_number", mobile_number);
        cmd.Parameters.AddWithValue("@graduation", graduation);
        cmd.Parameters.AddWithValue("@address", address);
        cmd.Parameters.AddWithValue("@reference", reference);

        cmd.ExecuteNonQuery();
    }

    context.Response.Redirect("/");
});

app.Run();