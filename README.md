How it Works?

I divided full system in 4 section.

Section-1:
User can create an account by providing mention information. Here ID is the Primary key. User must provide all required field information.

Section-2:
Second section is the Update/Edit section. Here user can modify any kind of information without ID.

Section-3:
In section 3 User can delete any account.

Section-4:
User can view details of an account. 

Section-5:
In section 5 user find the list of all account.
 

Code of Controller Page:

 public class TaskController : Controller
    {
        private SodingDBEntities db = new SodingDBEntities();

        // GET: /Task/
        public ActionResult Index()
        {
            return View(db.Table_UserInfo.ToList());
        }

        // GET: /Task/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_UserInfo table_userinfo = db.Table_UserInfo.Find(id);
            if (table_userinfo == null)
            {
                return HttpNotFound();
            }
            return View(table_userinfo);
        }

        // GET: /Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Description,Email")] Table_UserInfo table_userinfo)
        {
            if (ModelState.IsValid)
            {
                table_userinfo.DateCreated = DateTime.Now.Date;
                db.Table_UserInfo.Add(table_userinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table_userinfo);
        }

        // GET: /Task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_UserInfo table_userinfo = db.Table_UserInfo.Find(id);
            if (table_userinfo == null)
            {
                return HttpNotFound();
            }
            return View(table_userinfo);
        }

        // POST: /Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Description,Email")] Table_UserInfo table_userinfo)
        {
            if (ModelState.IsValid)
            {
                var getOldData = db.Table_UserInfo.Find(table_userinfo.Id);
                getOldData.Id = table_userinfo.Id;
                getOldData.Name = table_userinfo.Name;
                getOldData.Description = table_userinfo.Description;
                getOldData.Email = table_userinfo.Email;
                getOldData.DateUpdated = DateTime.Now.Date;
                //table_userinfo.DateCreated = getOldData.DateCreated;
                //table_userinfo.DateUpdated = DateTime.Now.Date;
                //db.Table_UserInfo.Add(table_userinfo);
                //db.Entry(table_userinfo).CurrentValues.SetValues(table_userinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table_userinfo);
        }

        // GET: /Task/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_UserInfo table_userinfo = db.Table_UserInfo.Find(id);
            if (table_userinfo == null)
            {
                return HttpNotFound();
            }
            return View(table_userinfo);
        }

        // POST: /Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table_UserInfo table_userinfo = db.Table_UserInfo.Find(id);
            db.Table_UserInfo.Remove(table_userinfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }


Code of model:

public partial class Table_UserInfo
    {
        
        [Display(Name = "USER ID:")]
        [Required(ErrorMessage = "ID is required")]
        public int Id { get; set; }
        [Display(Name = "NAME:")]
        [Required(ErrorMessage = "NAME is required")]
        public string Name { get; set; }
        [Display(Name = "DESCRIPTION:")]
        public string Description { get; set; }
        [Display(Name = "EMAIL:")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }


Code Of Create View:

@model SodingDemoApp.Table_UserInfo

@{
    ViewBag.Title = "Create";
}

<h2>Create</h2>


@using (Html.BeginForm()) 
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
        <h4>Table_UserInfo</h4>
        <hr />
        @Html.ValidationSummary(true)

        <div class="form-group">
            @Html.LabelFor(model => model.Id, new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Id)
                @Html.ValidationMessageFor(model => model.Id)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Name, new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Name)
                @Html.ValidationMessageFor(model => model.Name)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Description, new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Description)
                @Html.ValidationMessageFor(model => model.Description)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Email, new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Email)
                @Html.ValidationMessageFor(model => model.Email)
            </div>
        </div>

       

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Create" class="btn btn-default" />
            </div>
        </div>
    </div>
}

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
}


Code of Edit View:

@model SodingDemoApp.Table_UserInfo

@{
    ViewBag.Title = "Edit";
}

<h2>Edit</h2>


@using (Html.BeginForm())
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
        <h4>Table_UserInfo</h4>
        <hr />
        @Html.ValidationSummary(true)
        @Html.HiddenFor(model => model.Id)

        <div class="form-group">
            @Html.LabelFor(model => model.Name, new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Name)
                @Html.ValidationMessageFor(model => model.Name)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Description, new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Description)
                @Html.ValidationMessageFor(model => model.Description)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Email, new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Email)
                @Html.ValidationMessageFor(model => model.Email)
            </div>
        </div>

       

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Save" class="btn btn-default" />
            </div>
        </div>
    </div>
}

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
}


Code of Delete view:

@model SodingDemoApp.Table_UserInfo

@{
    ViewBag.Title = "Delete";
}

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>Table_UserInfo</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(model => model.Name)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.Name)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.Description)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.Description)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.Email)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.Email)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.DateCreated)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.DateCreated)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.DateUpdated)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.DateUpdated)
        </dd>

    </dl>

    @using (Html.BeginForm()) {
        @Html.AntiForgeryToken()

        <div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    }
</div>


Code Of list of item view:

@model IEnumerable<SodingDemoApp.Table_UserInfo>

@{
    ViewBag.Title = "Index";
}

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(model => model.Name)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Description)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Email)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.DateCreated)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.DateUpdated)
        </th>
        <th></th>
    </tr>

@foreach (var item in Model) {
    <tr>
        <td>
            @Html.DisplayFor(modelItem => item.Name)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Description)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Email)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.DateCreated)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.DateUpdated)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", new { id=item.Id }) |
            @Html.ActionLink("Details", "Details", new { id=item.Id }) |
            @Html.ActionLink("Delete", "Delete", new { id=item.Id })
        </td>
    </tr>
}

</table>
