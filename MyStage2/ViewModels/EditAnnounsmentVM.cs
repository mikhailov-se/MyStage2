using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStage2.Models;

namespace MyStage2.ViewModels
{
    public class EditAnnounsmentVm
    {
        public Announsment Announsment { get; set; }
        public List<SelectListItem> Users { get; set; }

        public int SelectedUserId { get; set; }
    }
}
