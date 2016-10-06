using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace ArtContest.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {

    }
    public class UserMetadata
    {
        [Remote("IsUserNameAvailable","Home",ErrorMessage ="UserName already in use")]
        public string UserName { get; set; }
    }
}