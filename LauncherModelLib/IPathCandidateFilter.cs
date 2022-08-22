﻿using LauncherModelLib.PathModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib
{
    /// <summary>
    /// Filterキーワードで一致するFilePathの候補を提案する
    /// </summary>
    public interface IPathCandidateFilter
    {
        List<IPath> Filter(string query);
    }
}
