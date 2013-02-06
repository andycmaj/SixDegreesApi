(function ($, Backbone, _, Common, Search) {

    Search.Model = Backbone.Model.extend({
    });

    Search.View = Backbone.View.extend({
        template: Common.tmpl('SearchForm'),
        
        events: {
            'click .searchbuttons button': 'doSearch'
        },
        
        initialize: function() {
            this.model = new Search.Model();
        },
        
        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            
            this._searchText = this.$('.searchfield');

            return this.$el;
        },

        doSearch: function (event) {
            this.model.set('query', this._searchText.val());
            this.model.fetch({
                success: function(model, response, options) {
                    console.log(response);
                }
            });
        }
    });
})(jQuery, Backbone, _, window.Common, window.Search = {});