<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DatosTaller.aspx.cs" Inherits="TalleresWeb.Web.UI.Account.DatosTaller" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="contenido">
            	<div class="row">
                	<div class="col-sm-12">
			            <h3>Mis Datos</h3>
                    </div>
                    <hr>
                    <form class="forms" method="post" action="./IngresarTramite.aspx" id="ctl01" autocomplete="off">
                        <div class="col-sm-12">
                            <h4>Personales:</h4>
                        </div>
                        <div class="col-sm-2 form-group">
                        	<label for="">Nombre:</label>
                            <input name="" type="text" value="Héctor Ricardo" id="" class="form-control" placeholder="" />
                        </div>
                        <div class="col-sm-2 form-group">
                        	<label for="">Apellido:</label>
                            <input name="" type="text" value="Bodo" id="" class="form-control" placeholder="" />
                        </div>
                        <div class="col-sm-2 form-group">
                        	<label for="">E-mail:</label>
                            <input name="" type="text" value="" id="" class="form-control" placeholder="" />
                        </div>
                        <div class="col-sm-2 form-group">
                        	<label for="tipo_dni">Tipo de Documento:</label>
                            <select name="" class="form-control" id="tipo_dni">
                                <option selected="selected" value="16834059-e4d3-418d-9ecf-14fbc740193f">DNI</option>
                                <option value="93249994-187a-47e4-a0cc-5d7f8e086283">LE</option>
                                <option value="70331dfc-352a-4123-b83e-a1d7c7297e10">CI</option>
                                <option value="1ba143b2-06bb-4857-b705-d792fd2b2d64">LC</option>
                                <option value="fd0675d6-1fb3-4b24-ab8e-d94f41bf71d7">CUIT</option>
                            </select>
                        </div>
                        <div class="col-sm-2 form-group">
                        	<label for="">Número de Documento:</label>
                            <input name="" type="text" value="" maxlength="11" id="" class="form-control" placeholder="" />
                        </div>
                        <div class="col-sm-2 form-group">
                        	<label for="">Clave:</label>
                            <input name="" type="text" value="" id="" class="form-control" placeholder="" />
                        </div>
                        
                        <div class="col-sm-12">
                            <h4>Taller:</h4>
                        </div>
                        <div class="col-sm-2 form-group">
                        	<label for="">Nombre:</label>
                            <input name="" type="text" value="Taller GNCenter" id="" class="form-control" placeholder="" />
                        </div>
                        <div class="col-sm-2 form-group">
                        	<label for="">Dirección:</label>
                            <input name="" type="text" value="" id="" class="form-control" placeholder="" />
                        </div>
                        <div class="col-sm-2 form-group">
                        	<label for="">Localidad:</label>
                            <select name="ctl00$MainContent$ctl05" class="form-control">
                                <option selected="selected" value="e4804cf9-e3f5-42f1-9444-8fb59d34274e">28 DE NOVIEMBRE   </option>
                                <option value="18e56896-d490-44f0-abd2-d610d339d5fe">ABASTO            </option>
                                <option value="30ac43fa-7375-4e20-b34f-1ffc6d0a4179">ACEBAL            </option>                                
                            </select>
                        </div>
                        <div class="col-sm-2 form-group">
                        	<label for="">Teléfono:</label>
                            <input name="" type="text" value="" id="" class="form-control" placeholder="" />
                        </div>
                        <div class="col-sm-2 form-group">
                        	<label for="">&nbsp;</label>
                        	<button type="button" class="btn btn-primary btn-block" data-dismiss="modal" onClick="">Guardar</button>
                    	</div>
                        <div class="col-sm-2 form-group">
                        	<label for="">&nbsp;</label>
                    		<button type="button" class="btn btn-danger btn-block" data-dismiss="modal">Cancelar</button>
                        </div>
                        
                    </form>
                    <p>&nbsp;</p>
                </div>
            </div>

</asp:Content>
