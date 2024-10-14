/**
 * @license Copyright (c) 2003-2022, CKSource Holding sp. z o.o. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	 config.language = 'fa';
	// config.uiColor = '#AADC6E';
	

	//------------ old method
	//config.filebrowserImageBrowseUrl = "Content/plugins/ckeditor/ImageBrowser.aspx";
	//config.filebrowserImageWindowWidth = 780;
	//config.filebrowserImageWindowHeight = 720;
	//config.filebrowserBrowseUrl = "Content/plugins/ckeditor/LinkBrowser.aspx";
	//config.filebrowserWindowWidth = 500;
	//config.filebrowserWindowHeight = 650;
	//------------ old method
	//var base_url = window.location;
	//alert(base_url);
	//alert(base_url.replace('/Admin/Home/Index', '') + "/FileManager/Main/_Index");
	config.filebrowserImageBrowseUrl = "/FileManager/Main/_Index";
	config.filebrowserImageWindowWidth = 1000;
	
	
};
