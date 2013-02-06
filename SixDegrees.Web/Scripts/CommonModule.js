(function (Handlebars, Common) {
    
    /**
     * Helper to render compiled handlebars templates by name (without specifying
     * template file extension).
     */
    Common.tmpl = function (templateName, data) {
        // Append the template file extension to the specified template name, since
        // template compilation builds function names based on template file names.
        var templateFunc = Handlebars.templates[templateName + ".html"];
        return data ? templateFunc(data) : templateFunc;
    };

})(Handlebars, window.Common = {});