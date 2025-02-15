using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using TelecomApp.Models.MetaData;
using System;
using System.ComponentModel.DataAnnotations;

namespace TelecomApp.Models
{
    [ModelMetadataType(typeof(UserMetaData))]
    public partial class User
    {
        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";
    }

    [ModelMetadataType(typeof(AdminMetaData))]
    public partial class Admin
    {
    }

    [ModelMetadataType(typeof(SellerMetaData))]
    public partial class Seller
    {
    }

    [ModelMetadataType(typeof(ClientMetaData))]
    public partial class Client
    {
    }

    [ModelMetadataType(typeof(BillMetaData))]
    public partial class Bill
    {
    }

    [ModelMetadataType(typeof(CallMetaData))]
    public partial class Call
    {
    }

    [ModelMetadataType(typeof(PhoneMetaData))]
    public partial class Phone
    {
    }

    [ModelMetadataType(typeof(ProgramMetaData))]
    public partial class PhoneProgram
    {
    }
}