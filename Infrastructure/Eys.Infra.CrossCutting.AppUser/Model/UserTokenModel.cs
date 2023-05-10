using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Infra.CrossCutting.AppUserIdentity.Model
{
    public class UserTokenModel
    {
        public string AccessToken { get; set; }

        public UserTokenModel(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
