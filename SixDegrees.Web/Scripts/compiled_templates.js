(function() {
  var template = Handlebars.template, templates = Handlebars.templates = Handlebars.templates || {};
templates['ListItem.html'] = template(function (Handlebars,depth0,helpers,partials,data) {
  helpers = helpers || Handlebars.helpers;
  


  return "﻿<div>list item</div>";});
templates['SearchForm.html'] = template(function (Handlebars,depth0,helpers,partials,data) {
  helpers = helpers || Handlebars.helpers;
  


  return "﻿<div id=\"searchform\">\r\n\r\n  <fieldset>\r\n    <input type=\"search\" class=\"searchfield\" />\r\n  </fieldset>\r\n  <fieldset class=\"searchbuttons\">\r\n    <button id=\"movie\">\r\n      <span>Search Movies</span>\r\n    </button>\r\n    <button id=\"person\">\r\n      <span>Search People</span>\r\n    </button>\r\n  </fieldset>\r\n\r\n</div>";});
})();