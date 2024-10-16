﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Common.Models
{
    public class ProjectModel : CommonModel
    {
        public ProjectStatus Status { get; set; }
        public int? AdminId { get; set; }
        public List<UserModel> AllUsers { get; set; } = new List<UserModel>();
        public List<DeskModel> AllDesks { get; set; } = new List<DeskModel>();
    }
}
