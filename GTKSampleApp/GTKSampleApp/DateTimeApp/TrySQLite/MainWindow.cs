using System;
using Gtk;
using TrySQLite;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();

    }

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnButton3Clicked (object sender, EventArgs e)
	{

		textview1.Buffer.Text = "New Text";

		using (var db = new BloggingContext())
		{
			db.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
			var count = db.SaveChanges();
			textview1.Buffer.Text = textview1.Buffer.Text + string.Format("{0} records saved to database", count);

            List<Blog> blogs = db.Set<Blog>().ToListAsync().Result;

            foreach (var blog in blogs)
			{
				textview1.Buffer.Text = textview1.Buffer.Text + string.Format(" - {0}", blog.Url);
			}
		}
	}
}


