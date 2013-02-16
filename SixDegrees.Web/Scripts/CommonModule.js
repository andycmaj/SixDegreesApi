(function (Backbone, Marionaette, Handlebars, Common) {
  
    var App = window.App = new Marionette.Application();

    App.addRegions({
        "searchRegion": "#searchcontainer"
    });
    
    /**
     * Helper to render compiled handlebars templates by name (without specifying
     * template file extension).
     */
    Marionette.Renderer.render = function(templateName, data) {
        // Append the template file extension to the specified template name, since
        // template compilation builds function names based on template file names.
        var templateFunc = Handlebars.templates[templateName + ".html"];
        return data ? templateFunc(data) : templateFunc;
    };

})(Backbone, Marionette, Handlebars, window.Common = {});