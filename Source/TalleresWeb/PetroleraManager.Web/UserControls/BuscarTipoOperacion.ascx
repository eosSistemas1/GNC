﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuscarTipoOperacion.ascx.cs" Inherits="PetroleraManager.Web.UserControls.BuscarTipoOperacion" %>
<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<%--<link rel="stylesheet" href="/resources/demos/style.css">--%>
<style>
    .custom-comboboxTipoOperaciones {        
        position: relative;
        display: inline-block;
    }

    .custom-comboboxTipoOperaciones-toggle {
        position: absolute;
        top: 0;
        bottom: 0;
        margin-left: -1px;
        padding: 0;
    }

    .custom-comboboxTipoOperaciones-input {
        margin: 0;
        padding: 5px 10px;
    }
</style>
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<script>
    $(function () {
        $.widget("custom.comboboxTipoOperaciones", {
            _create: function () {
                this.wrapper = $("<span>")
					.addClass("custom-comboboxTipoOperaciones")
					.insertAfter(this.element);

                this.element.hide();
                this._createAutocomplete();
                this._createShowAllButton();
            },

            _createAutocomplete: function () {
                var selected = this.element.children(":selected"),
					value = selected.val() ? selected.text() : "";

                this.input = $("<input>")
					.appendTo(this.wrapper)
					.val(value)
					.attr("title", "")
					.addClass("custom-comboboxTipoOperaciones-input ui-widget ui-widget-content ui-state-default ui-corner-left")
					.autocomplete({
					    delay: 0,
					    minLength: 0,
					    source: $.proxy(this, "_source")
					})
					.tooltip({
					    classes: {
					        "ui-tooltip": "ui-state-highlight"
					    }
					});

                this._on(this.input, {
                    autocompleteselect: function (event, ui) {
                        ui.item.option.selected = true;
                        this._trigger("select", event, {
                            item: ui.item.option
                        });
                    },

                    autocompletechange: "_removeIfInvalid"
                });
            },

            _createShowAllButton: function () {
                var input = this.input,
					wasOpen = false;

                $("<a>")
					.attr("tabIndex", -1)
					.attr("title", "Show All Items")
					.tooltip()
					.appendTo(this.wrapper)
					.button({
					    icons: {
					        primary: "ui-icon-triangle-1-s"
					    },
					    text: false
					})
					.removeClass("ui-corner-all")
					.addClass("custom-comboboxTipoOperaciones-toggle ui-corner-right")
					.on("mousedown", function () {
					    wasOpen = input.autocomplete("widget").is(":visible");
					})
					.on("click", function () {
					    input.trigger("focus");

					    // Close if already visible
					    if (wasOpen) {
					        return;
					    }

					    // Pass empty string as value to search for, displaying all results
					    input.autocomplete("search", "");
					});
            },

            _source: function (request, response) {
                var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                response(this.element.children("option").map(function () {
                    var text = $(this).text();
                    if (this.value && (!request.term || matcher.test(text)))
                        return {
                            label: text,
                            value: text,
                            option: this
                        };
                }));
            },

            _removeIfInvalid: function (event, ui) {

                // Selected an item, nothing to do
                if (ui.item) {
                    return;
                }

                // Search for a match (case-insensitive)
                var value = this.input.val(),
					valueLowerCase = value.toLowerCase(),
					valid = false;
                this.element.children("option").each(function () {
                    if ($(this).text().toLowerCase() === valueLowerCase) {
                        this.selected = valid = true;
                        return false;
                    }
                });

                // Found a match, nothing to do
                if (valid) {
                    return;
                }

                // Remove invalid value
                this.input
					.val("")
					.attr("title", value + " didn't match any item")
					.tooltip("open");
                this.element.val("");
                this._delay(function () {
                    this.input.tooltip("close").attr("title", "");
                }, 2500);
                this.input.autocomplete("instance").term = "";
            },

            _destroy: function () {
                this.wrapper.remove();
                this.element.show();
            }
        });

        $("#comboboxTipoOperaciones").comboboxTipoOperaciones();
        $("#toggle").on("click", function () {
            $("#comboboxTipoOperaciones").toggle();
        });      
    });
</script>


<div>
    <label>TipoOperacion: </label>
    <select id="comboboxTipoOperaciones" runat="server" clientidmode="static" ></select>
</div>
