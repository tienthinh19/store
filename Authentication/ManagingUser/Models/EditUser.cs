﻿using System;
using System.Collections.Generic;

namespace Authentication.Models;

public class EditUser
{
    public string Id { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }

}