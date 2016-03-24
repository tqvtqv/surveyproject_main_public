using ADBrowser5.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADBrowser5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult SearchOU(ADSearchFilter model)
        {
            DirectoryEntry user_entry = null;
            try
            {
                //DirectoryEntry entry = new DirectoryEntry("LDAP://" + ldapserver + "/" + ldapbasedn, ADAdminAccount, ADAdminPassword, AuthenticationTypes.Secure);
                DirectoryEntry entry = new DirectoryEntry("LDAP://10.1.2.4:389/DC=cqtd,DC=vnpt,DC=vn", "tranquocviet", "g!vkGox2a", AuthenticationTypes.Secure);
                object nativeObject = entry.NativeObject;
                DirectorySearcher ds = new DirectorySearcher(entry);
                // ds.PropertiesToLoad.Add("" + Property.mobile + "");
                //ds.PropertiesToLoad.Add("" + Property.userPassword + "");
                ds.SearchScope = SearchScope.Subtree;
                ds.PageSize = 100;
                ds.CacheResults = false;
                ds.Filter = "(OU=" + model.OUFilter + ")";

                // SearchResult result = ds.FindOne();
                SearchResultCollection lst_res = ds.FindAll();

                model.Results = ds.FindAll();
                model.Paths = new List<String>();
                if (lst_res.Count == 0) { }
                //MessageBox.Show("Ko tim thay user");
                else
                {
                    for (int i = 0; i < lst_res.Count; i++)
                    {
                        SearchResult result = lst_res[i];
                        string path = "cqtd.vnpt.vn";
                        // Tim thay user
                        user_entry = result.GetDirectoryEntry();

                        DirectoryEntry parent = user_entry.Parent;
                        if (parent.Name != "DC=cqtd")
                        {
                            string parent_path = "";
                            while (parent.Name != "DC=cqtd")
                            {
                                parent_path = parent.Name.Remove(0, 3) + "/" + parent_path;
                                parent = parent.Parent;
                            }
                            path = path + "/" + parent_path + model.OUFilter;
                        }
                        else
                            path = path + "/" + model.OUFilter;
                        model.Paths.Add(path);
                    }

                }


            }
            catch (DirectoryServicesCOMException) { }
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult SearchCN(ADSearchFilter model)
        {
            DirectoryEntry user_entry = null;
            try
            {
                //DirectoryEntry entry = new DirectoryEntry("LDAP://" + ldapserver + "/" + ldapbasedn, ADAdminAccount, ADAdminPassword, AuthenticationTypes.Secure);
                DirectoryEntry entry = new DirectoryEntry("LDAP://10.1.2.4:389/DC=cqtd,DC=vnpt,DC=vn", "tranquocviet", "g!vkGox2a", AuthenticationTypes.Secure);
                object nativeObject = entry.NativeObject;
                DirectorySearcher ds = new DirectorySearcher(entry);
                // ds.PropertiesToLoad.Add("" + Property.mobile + "");
                //ds.PropertiesToLoad.Add("" + Property.userPassword + "");
                ds.SearchScope = SearchScope.Subtree;
                ds.PageSize = 100;
                ds.CacheResults = false;
                ds.Filter = "(CN=" + model.CNFilter + ")";

                // SearchResult result = ds.FindOne();
                SearchResultCollection lst_res = ds.FindAll();

                model.Results = ds.FindAll();
                model.Paths = new List<String>();
                if (lst_res.Count == 0) { }
                //MessageBox.Show("Ko tim thay user");
                else
                {
                    for (int i = 0; i < lst_res.Count; i++)
                    {
                        SearchResult result = lst_res[i];
                        string path = "cqtd.vnpt.vn";
                        // Tim thay user
                        user_entry = result.GetDirectoryEntry();

                        DirectoryEntry parent = user_entry.Parent;
                        if (parent.Name != "DC=cqtd")
                        {
                            string parent_path = "";
                            while (parent.Name != "DC=cqtd")
                            {
                                parent_path = parent.Name.Remove(0, 3) + "/" + parent_path;
                                parent = parent.Parent;
                            }
                            path = path + "/" + parent_path + model.OUFilter;
                        }
                        else
                            path = path + "/" + model.OUFilter;
                        model.Paths.Add(path);
                    }

                }


            }
            catch (DirectoryServicesCOMException) { }
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult SearchAD(ADSearchFilter model)
        {

            try
            {
                //DirectoryEntry entry = new DirectoryEntry("LDAP://" + ldapserver + "/" + ldapbasedn, ADAdminAccount, ADAdminPassword, AuthenticationTypes.Secure);
                DirectoryEntry entry = new DirectoryEntry("LDAP://10.1.2.4:389/DC=cqtd,DC=vnpt,DC=vn", "tranquocviet", "g!vkGox2a", AuthenticationTypes.Secure);
                object nativeObject = entry.NativeObject;
                DirectorySearcher ds = new DirectorySearcher(entry);
                // ds.PropertiesToLoad.Add("" + Property.mobile + "");
                //ds.PropertiesToLoad.Add("" + Property.userPassword + "");
                ds.SearchScope = SearchScope.Subtree;
                ds.PageSize = 100;
                ds.CacheResults = false;
                ds.Filter = "(" + model.CNFilter + ")";
                model.Results = ds.FindAll();

            }
            catch (DirectoryServicesCOMException) { }
            return View("Index", model);
        }
    }
}