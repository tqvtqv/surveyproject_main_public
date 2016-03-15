using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.UserProvider;
using Votations.NSurvey.WebAdmin;

namespace Votations.NSurvey.WebAdmin.Code
{
    public class MyClaimsTransformer: ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (!incomingPrincipal.Identity.IsAuthenticated)
            {
                return base.Authenticate(resourceName, incomingPrincipal);
            }
            int? id = new Users().GetUserByIdFromUserName(incomingPrincipal.Identity.Name);
            if ((id ?? 0) > 0)
            {
                var sec = new LoginSecurity();
                var authUser = new Users().GetUserById(id ?? 0);
                UserSettingData userSettings = new Users().GetUserSettings(authUser.Users[0].UserId);

                if (userSettings.UserSettings.Rows.Count > 0)
                {
                    System.Text.StringBuilder userInfos = new System.Text.StringBuilder();
                    userInfos.Append(authUser.Users[0].UserName + ",");
                    userInfos.Append(authUser.Users[0].UserId + ",");
                    userInfos.Append(authUser.Users[0].FirstName + ",");
                    userInfos.Append(authUser.Users[0].LastName + ",");
                    userInfos.Append(authUser.Users[0].Email + ",");
                    userInfos.Append(userSettings.UserSettings[0].IsAdmin + ",");
                    userInfos.Append(userSettings.UserSettings[0].GlobalSurveyAccess);

                    userInfos.Append("|");

                    int[] userRights = new Users().GetUserSecurityRights(authUser.Users[0].UserId);
                    for (int i = 0; i < userRights.Length; i++)
                    {

                        userInfos.Append(userRights[i].ToString());
                        if (i + 1 < userRights.Length)
                        {
                            userInfos.Append(",");
                        }

                    }

                    ((ClaimsIdentity)incomingPrincipal.Identity).AddClaim(new Claim(Votations.NSurvey.Constants.Constants.MyCustomClaimType, userInfos.ToString()));



                    //FormsAuthentication.SetAuthCookie(userInfos.ToString(), false);
                    //NSurveyContext.Current.User = UserFactory.Create().CreatePrincipal(userInfos.ToString());
                    var x = UserFactory.Create().CreatePrincipal((ClaimsIdentity)incomingPrincipal.Identity);


                    //((PageBase)Page).SelectedFolderId = null;
                    // ((Wap)this.Master).RebuildTree();
                    UINavigator.NavigateToFirstAccess(x, -1);
                }
            }

            return incomingPrincipal;
        }
    }
}
