using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace PL.Fwk.Presentation.Web.Controls
{
    public class PLGrid : ControlBase
    {

        protected override void OnInit(EventArgs e)
        {/*
            ClientScriptManager cs = Page.ClientScript;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("<script type='text/javascript' src='js/jquery-1.5.2.min.js'></script>");
            sb.AppendLine("<script type='text/javascript' src='js/grid.locale-es.js'></script>");
            sb.AppendLine("<script type='text/javascript' src='js/jquery.jqGrid.min.js'></script>");
            sb.AppendLine("<script type='text/javascript' src='js/grid.base.js'></script>");
            sb.AppendLine("<script type='text/javascript' src='js/grid.common.js'></script>");
            sb.AppendLine("<script type='text/javascript' src='js/grid.formedit.js'></script>");
            sb.AppendLine("<script type='text/javascript' src='js/jquery.fmatter.js'></script>");
            sb.AppendLine("<script type='text/javascript' src='js/jsonXml.js'></script>");
            sb.AppendLine(" <script type='text/javascript' src='js/jquery.tablednd.js'></script>");
            sb.AppendLine("<script type='text/javascript' src='js/ui.multiselect.js'></script>");
            sb.AppendLine("<script type='text/javascript' src='js/grid.inlinedit.js'></script>");
            sb.AppendLine("<script type='text/javascript' src='js/jQDNR.js'></script>");
            sb.AppendLine("<script type='text/javascript' src='js/jqModal.js'></script>");

           

       sb.Append(@"     <script type='text/javascript'> 
        var $table = $('#grid');
        var lastSel;
        var colMode = []; 
        jQuery(document).ready(function () {
            //aca es modificacion
            $.ajax({
                dataType: 'json',
                type: 'post',
                url: 'http://localhost:1962/Webservices/LoadColumns.asmx/GetColumns',
                data: '{}',
                contentType: 'application/json;',
                async: false, //esto es requerido, de otra forma el jqgrid se cargaria antes que el grid
                success: function (data) {
                    var persons = JSON.parse(data.d);
                    $.each(persons, function (index, persona) {
                        colMode.push({ name: persona.Name, index: persona.index,label: persona.label, width: persona.width, align: persona.align, editable: persona.editable, edittype: persona.editType, editrules: { edithidden: true }, hidden: false });
                     })

                } //or

            }),
            //acá
            $('#grid').jqGrid(
        {
            datatype: function () {
                $.ajax(
                {
                    url: 'http://localhost:1962/Webservices/LoadData.asmx/GetPersons', //PageMethod

                    data: '{'pPageSize':'' + $('#grid').getGridParam('rowNum') +
                    '','pCurrentPage':'' + $('#grid').getGridParam('page') +
                    '','pSortColumn':'' + $('#grid').getGridParam('sortname') +
                    '','pSortOrder':'' + $('#grid').getGridParam('sortorder') + ''}', //PageMethod Parametros de entrada

                    dataType: 'json',
                    type: 'post',
                    contentType: 'application/json; charset=utf-8',
                    complete: function (jsondata, stat) {
                        if (stat == 'success')
                            jQuery('#grid')[0].addJSONData(JSON.parse(jsondata.responseText).d);
                        else
                            alert(JSON.parse(jsondata.responseText).Message);
                    }
                });
            },
            jsonReader: //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
            root: 'Items',
            page: 'CurrentPage',
            total: 'PageCount',
            records: 'RecordCount',
            repeatitems: true,
            cell: 'Row',
            id: 'ID' //index of the column with the PK in it    
        },

        colModel: colMode,

        pager: '#pager', //Pager.
        loadtext: 'Cargando datos...',
        recordtext: '{0} - {1} de {2} elementos',
        emptyrecords: 'No hay resultados',
        pgtext: 'Pág: {0} de {1}', //Paging input control text format.
        rowNum: '10', // PageSize.
        rowList: [10, 20, 30], //Variable PageSize DropDownList. 
        viewrecords: true, //Show the RecordCount in the pager.
        multiselect: true,
        sortname: 'Name', //Default SortColumn
        sortorder: 'asc', //Default SortOrder.
        width: '800',
        height: '230',
        caption: 'Personas',
        ondblClickRow: function (id) {
            gdCustomers.restoreRow(lastSel);
            gdCustomers.editRow(id, true);
            lastSel = id;
        }
    }).navGrid('#pager', { edit: true, add: true, search: true, del: true },
    { url: 'http://localhost:1962/Webservices/ProcessData.asmx/EditData', closeAfterEdit: true },
    { url: 'http://localhost:1962/Webservices/ProcessData.asmx/EditData', closeAfterAdd: true },
    { url: 'http://localhost:1962/Webservices/ProcessData.asmx/DeleteData' });


            jQuery.extend(jQuery.jgrid.edit, {
                ajaxEditOptions: { contentType: 'application/json' },
                recreateForm: true,
                serializeEditData: function (postData) {
                    return JSON.stringify(postData);
                }
            });

            jQuery.extend(jQuery.jgrid.del, {
                ajaxDelOptions: { contentType: 'application/json' },
                serializeDelData: function (postData) {

                    return JSON.stringify(postData);
                }
            });



     });
       
    </script>");


         if (!cs.IsStartupScriptRegistered("JSScript"))
            {
                cs.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
            }


            Table tabla = new Table();
            tabla.ID = "grid";
            Panel panel = new Panel();
            panel.ID = "pager";

            this.Controls.Add(tabla);
            this.Controls.Add(panel);
        }* */

        }
    }
}
