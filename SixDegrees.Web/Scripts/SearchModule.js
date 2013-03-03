var App = window.App = new Marionette.Application();
App.addRegions({
    mainRegion: "#main"
});

App.module("SearchModule", function (Mod, App, Backbone, Marionette, $, _) {

    // Define a view to show
    // ---------------------
    var SearchFormView = Marionette.ItemView.extend({
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
                },
                self = this;

            this.model.fetch({
                data: searchParams,
                success: function (model, response, options) {
                    self.trigger('search:success', model);
                }
            });
        }
    });

    var DegreeModel = Backbone.Model.extend({
        initialize: function(o) {
            console.trace('init');
            console.debug(o);
            
            console.groupEnd();
        },
        
        parse: function (m) {
            console.group('degreeModel');
            console.trace('parse');
            console.debug(m);

            return _.extend(m, {
                Children: new Backbone.Collection(m.Children)
            });
        }
        
    });

    var DegreeCollection = Backbone.Collection.extend({
        url: '/api/degree',
        model: DegreeModel
    });

    var DegreeItemView = Marionette.ItemView.extend({
        template: 'DegreeItem',

        initialize: function() {

        },
        
        events: {
            'click': 'showChildren'
        },
        
        showChildren: function(evt) {
            var children = this.model.get('Children');
            
        }
    });

    var SearchResultsModel = Backbone.Collection.extend({
        url: '/api/search',
        model: DegreeModel
    });

    var SearchResultsView = Marionette.CollectionView.extend({
        itemView: DegreeItemView
    });
    
    // TODO: Bind a collectionview to the same searchresultsmodel, when search:success, hide the form, show the collectionview.

    // Define a controller to run this module
    // --------------------------------------
    var SearchController = Marionette.Controller.extend({

        initialize: function (options) {
            this.hostRegion = options.region;
            
            var searchResultsModel = new SearchResultsModel();
            
            this.formView = new SearchFormView({
                model: searchResultsModel
            });
            this.listenTo(this.formView, "search:success", _.bind(this.showSearchResults, this));

            this.resultsView = new SearchResultsView({
                collection: searchResultsModel
            });
            
        },
        
        showSearchResults: function() {
            this.layout.results.show(this.resultsView);
        },

        show: function () {
            this.layout = new SearchLayout();
            this.layout.render();
            
            this.hostRegion.show(this.layout);
            
            this.layout.form.show(this.formView);
            //this.layout.results.attachView(this.resultsView);
        }

    });
    
    var SearchLayout = Marionette.Layout.extend({
        template: "SearchLayout",

        regions: {
            form: "#form",
            results: "#results"
        }
    });


    // Initialize this module when the app starts
    // ------------------------------------------

    Mod.addInitializer(function () {
        Mod.controller = new SearchController({
            region: App.mainRegion
        });
        Mod.controller.show();
    });
});