    App.module("SearchModule", function (Mod, App, Backbone, Marionette, $, _) {

        // Define a view to show
        // ---------------------
        var SearchView = Marionette.ItemView.extend({
            template: "SearchForm",
            
            ui: {
                searchField: ".searchfield"
            },
            
            events: {
                'click .searchbuttons button': 'doSearch'
            },
            
            doSearch: function (event) {
                var searchType = $(event.currentTarget).data('searchtype'),
                    searchParams = {
                        'query': this.ui.searchField.val(),
                        'type': searchType
                    };
                
                this.model.fetch({
                    data: searchParams,
                    success: function (model, response, options) {
                        console.log(response);
                    }
                });
            }
        });

        var SearchModel = Backbone.Collection.extend({
            url: function() {
                return "/api/search";
            }
        });
        
        // Define a controller to run this module
        // --------------------------------------
        var SearchController = Marionette.Controller.extend({

            initialize: function (options) {
                this.region = options.region;
            },

            show: function () {
                var model = new SearchModel();

                var view = new SearchView({
                    model: model
                });

                this.region.show(view);
            }

        });


        // Initialize this module when the app starts
        // ------------------------------------------

        Mod.addInitializer(function () {
            Mod.controller = new SearchController({
                region: App.searchRegion
            });
            Mod.controller.show();
        });
    });