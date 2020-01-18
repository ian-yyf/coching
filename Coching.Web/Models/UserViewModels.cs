using Public.Model.Data;
using Public.Model.Front;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coching.Web.Models
{
    public class UserItemViewModel : PopupItemViewModel<UserData, FUser>
    {
        public UserItemViewModel()
        {

        }

        public UserItemViewModel(Guid keyGuid, string actionName, string actionTitle, UserData oldData, string callback)
           : base(keyGuid, actionName, actionTitle, oldData, callback)
        {
            Name = oldData.Name;
            Header = oldData.Header;
        }

        [Required]
        [Display(Name = "昵称")]
        public string Name { get; set; }
        [Display(Name = "头像")]
        public string Header { get; set; }
    }
}
