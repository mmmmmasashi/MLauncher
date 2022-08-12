using AutoCompleteTextBox.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherIF
{
    //MLauncherIFは、循環参照解消用のInterfaceのプロジェクト。
    //MLauncherがもとめるIFを定義するライブラリ。
    //Interface以外は置かないこと

    public interface IPathSuggestionService : ISuggestionProvider
    {
    }
}
