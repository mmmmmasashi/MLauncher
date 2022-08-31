# MLauncherとは
ファイル/フォルダ/URLのランチャーです。パスを登録しておくと、検索でき、Enterで起動します(URLの場合はブラウザが起動します)
![](Document/demo_search.gif)

# 注意点
- 開発中の段階で、正式リリース前です。
- 追加したい機能/修正のリストは[MLauncherApp/TODO.md](MLauncherApp/TODO.md)を参照ください。

# 使い方
- 基本操作
    - Ctrl+Shift+Zキーで前面に表示
- ランチャーへの登録
	- 覚えておきたいファイルやフォルダをドラッグ&ドロップして登録する
	- もしくはパスを入力してEnterで登録する
	- URLも同様に登録可能
- 起動する
    - 半角英数字でパス名を検索可能
	- スペース区切りでAND検索も加納
	- Enterでそのファイルを起動
	- Ctrl+Enterで対象の親のディレクトリを起動

# 特徴
- 半角英数字での検索に対応しています(migemoによる)
- URLにもファイル/フォルダパスにも対応
- パーセントエンコーディングされたURLは、エンコーディング前の日本語で検索可能です。
	- 例)
    	- Wikipediaの"パーセントエンコーディング"のページは以下の通りパーセンテージエンコーディングされます。
        	- "[https://ja.wikipedia.org/wiki/%E3%83%91%E3%83%BC%E3%82%BB%E3%83%B3%E3%83%88%E3%82%A8%E3%83%B3%E3%82%B3%E3%83%BC%E3%83%87%E3%82%A3%E3%83%B3%E3%82%B0](https://ja.wikipedia.org/wiki/%E3%83%91%E3%83%BC%E3%82%BB%E3%83%B3%E3%83%88%E3%82%A8%E3%83%B3%E3%82%B3%E3%83%BC%E3%83%87%E3%82%A3%E3%83%B3%E3%82%B0)"
		- 検索では"パーセントエンコーディング"という文字列でヒットします。
    		- "[https://ja.wikipedia.org/wiki/パーセントエンコーディング](https://ja.wikipedia.org/wiki/パーセントエンコーディング)"
	- 目的
    	- SharePointなどのクラウド系サービスで、URLに日本語を含むファイルを利用する場合を想定しています。この場合、パスを示すURLがパーセンテージエンコーディングされた文字列となるため。

# 利用しているOSS一覧
| 名称 | URL |
| --- | --- |
| migemo | https://github.com/koron/cmigemo |
| Prism.Unity | https://github.com/PrismLibrary/Prism |
| MaterialDesignThemes | https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit|
| MahApps.Metro | https://github.com/MahApps/MahApps.Metro |
| AutoCompleteTextBox | https://github.com/quicoli/WPF-AutoComplete-TextBox |



# License
The source code is licensed MIT. The website content is licensed CC BY 4.0,see LICENSE.