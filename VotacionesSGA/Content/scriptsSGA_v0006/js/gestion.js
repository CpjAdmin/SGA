var tipo = "D";
var reincidente = 'N';
var myInterval;
var objtabla = $('#tablaGestion');
var tabla;
var clock;
var nombreReporte = 'Lista de Delegados';
var DatosG = {
    "Orden" : 0,
    "Num_paleta": 0,
    "Cedula": '',
    "Nombre": '',
    "Usuario": '',
    "Tiempo": '',
    "Terminal": ''

};
var id_papeleta;
var paletaO; //Guarda el numero de paleta real de los candidatos ya que en el grid se muestra la posición del candidato en la papeleta.
var cedulaO; //Guarda la cédula del Delegado o Candidato del DataTable
var idPaleta;
var listaPaletas;

//*** Define Intervalo en Segundos para el Semaforo.
var SegDelegado1 = 180;
var SegDelegado2 = 120;
var SegCandidato = 60;

//*** #1 - Inicio
$(document).ready(function () {

    $('.card-title').removeClass('float');
    //*** 1.1 - Iniciar Contador
    clock = $('.clock-1').FlipClock({
        clockFace: 'MinuteCounter',
        language: 'Spanish',
        autoPlay: false,
        autoStart: false
    });

    //*** 1.2 - Modal Tipo
    $('#modal-lg-tipo-gestion').modal({
        backdrop: 'static',
        keyboard: false
    }, 'show');

    //*** 1.3 - Botón Aceptar del Modal Tipo
    $(document).on('click', '#btnSeleccionar', function () {

        tipo = $("#selTipo").val();
        //*** 1.3.1 - Tipo Delegados
        if (tipo === 'D') {
            CargarTablaGestion();
            $("#btnPapeletas").remove();

        } else {
            //*** 1.3.2 -  Tipo Candidatos
            $("#orden").text("Posición");

            //*** 1.3.3 - Obtener Papeletas
            obtenerPapeletas();

            $("#frmPaleta").remove();

        }
        // Ocultar Modal Tipo
        $('#modal-lg-tipo-gestion').modal('hide');
    });

    //*** 1.4 - Botón Cancelar del Modal Tipo
    $(document).on('click', '#btnCancelar', function () {

        //*** 1.4.1 - Redireccionar a la Página de Inicio del usuario
        redirect(paginaInicio);
    });

    //*** 1.5 - Tecla ENTER del TextBox ( Número de Paleta )
    $('#numPapeleta').on("keypress", function (e) {

        if (e.keyCode === 13) {

            if ($("#numPapeleta").val() === '0' || $("#numPapeleta").val() === '') {
                return false;
            } else {

                //*** 1.5.1 - Ingresar Registro de Delegado
                var oPaleta = $("#numPapeleta").val();

                // Verificar si existe la paleta ( -1 = No encontrado )
                if (jQuery.inArray(parseInt(oPaleta), listaPaletas) === -1) {
                    IngresarGestionDelegado(); 
                } else {
                    alertaInfoMin("LA PALETA YA EXISTE EN LA LISTA, PALETA: " + oPaleta);
                }
            }

            return false;
        }
    });

    //*** 1.6 - Botón Iniciar Contador de Tiempo
    $(document).on('click', '#btnIniciarT', function () {

        //*** Si la tabla está vacian, no hace nada
        if (!tabla.data().any()) {
            return false;
        }

        //*** ID de la Paleta Delegado o Posición del Candidato
        idPaleta = $('#tablaGestion').children('tbody').children('tr:first').find('td:eq(0)').text();

        //*** Deshabilitar botones
        $("#btnIniciarT").prop("disabled", true);
        $("#btnFinT").prop("disabled", false);
        $(".btn-papeletas").prop("disabled", true);
      
        //*** Texto del registro seleccionado e iniciado
        $('#palabra').text('PALETA  ' + $('#tablaGestion').children('tbody').children('tr:first').find('td:eq(0)').text() + ' - ( ' + $('#tablaGestion').children('tbody').children('tr:first').find('td:eq(2)').text() + ' )');

        //*** Valores de los atributos idR y idO
        reincidente = $('#tablaGestion').children('tbody').children('tr:first').find('td:eq(0) p').attr('data-idR');
        paletaO = $('#tablaGestion').children('tbody').children('tr:first').find('td:eq(0) p').attr('data-idO');
        cedulaO = $('#tablaGestion').children('tbody').children('tr:first').find('td:eq(1)').text();

        //*** 1.6.1 - Tipo de Registro ( Delegado )
        if (tipo === 'D') {
            //*** Verifica si es Reincidente
            if (reincidente === 'N') {

                //*** Define el Intervalo en Segundos
                myInterval = setInterval(function () {
                    if (clock.getTime().time >= SegDelegado1) {
                        $(".red").css("opacity", "1");
                    }
                }, 1000);

                //*** Iniciar Contador
                clock.start(function () { });
                
            } else {
                //*** Define el Intervalo en Segundos
                myInterval = setInterval(function () {
                    if (clock.getTime().time >= SegDelegado2) {
                        $(".red").css("opacity", "1");
                        clearInterval(myInterval);
                    }
                }, 1000);

                //*** Iniciar Contador
                clock.start(function () { });
            }
        } else {
        //*** Tipo de Registro ( Candidato )

            //*** Define el Intervalo en Segundos
            myInterval = setInterval(function () {
                if (clock.getTime().time >= SegCandidato) {
                    $(".red").css("opacity", "1");
                    clearInterval(myInterval);
                }
            }, 1000);

            //*** Iniciar Contador
            clock.start(function () {});
        }

        //*** 1.6.2 - Animación de Semaforo
        activarActivaDesactivaA('A', reincidente);

    });

    //*** 1.7 - Tipo de Registro ( Delegado )
    $(document).on('click', '#btnFinT', function () {

        //*** 1.7.1 - Reiniciar Variables
        $('#palabra').text('Esperando...');
        $("#btnIniciarT").prop("disabled", false);
        $(".btn-papeletas").prop("disabled", false);
        $("#btnFinT").prop("disabled", true);

        var time = clock.getTime().time;

        clock.stop(function () { });

        clock.reset(function () { });

        clearInterval(myInterval);

        //*** 1.7.2 - Actualizar Gestión de Uso de la Palabra
        if (tipo === 'D') {
            ActualizarGestion(time);
        } else {
            IngresarGestionC(time);
        }

        activarActivaDesactivaA('D', reincidente);
        
    });

    //*** 1.8 - Botón Reiniciar Contador de Tiempo
    $(document).on('click', '#reset', function () {

        $('#palabra').text('Esperando...');
        $("#btnIniciarT").prop("disabled", false);
        $(".btn-papeletas").prop("disabled", false);
        $("#btnFinT").prop("disabled", true);

        var time = clock.getTime();
        clock.stop(function () { });
        clock.reset(function () { });

        clearInterval(myInterval);

        activarActivaDesactivaA('D', reincidente);

    });

    //*** 1.9 - Botón Borrar Registro
    $(document).on('click', '.tooltipImgB', function () {

        //*** 1.9.1 - Usuario a Borrar
        var paleta = $(this).closest("tr").find('td:eq(0)').text();
        cedulaO = $(this).closest("tr").find('td:eq(1)').text();
        var fila = $(this).closest("tr");

        Swal.close();

        Swal.fire({
            title: 'Eliminar Datos',
            html: '¿Desea Eliminar la paleta <b>' + paleta + '</b> de la lista?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then(function (result) {

            //*** 1.9.2 - Validar resultado
            if (result.value) {

                if (tipo === 'D') {
                    //*** 1.9.2.1 - Eliminar Delegado
                    EliminarGestionDelegado(paleta);
                } else {
                    //*** 1.9.2.2 - Eliminar Candidato
                    fila.remove();
                }
            }
        });
    });

});
//*** #2 - Cargar Tabla de Delegados
function CargarTablaGestion() {
    //*** 2.1 - Mostrar Cargando
    MostrarCargando();
    //*** 2.2 - Ajax Cargar Delegados
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Gestion/ListaGestion',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Uso de la Palabra - Delegados", mensajeError);
            CerrarCargando();
        },
        success: function (data) {

            // Cargar paletas en listaPaletas para validar duplicación
            listaPaletas = [];
            data.map(function (element) {
                listaPaletas.push(element.Num_paleta);
            });

            //*** 2.2.1 - Cargar Filas
            CargarFilas(data);
            //*** 2.2.2 - Cerrar Cargando
            CerrarCargando();
        }
    });
}
//*** (2.2.2) #3 - Cargar Tabla de Delegados
function CargarFilas(array) {

    var filas = [];

    //*** 3.1 - Cargar Filas
    array.map(function (element) {
        filas.push([
            element.Orden,
            TipoPapeletaPosicion(element.Reincide, element.Num_paleta, element.Num_posicion),
            element.Cedula,
            element.Nombre,
            '<img class="tooltipImgB" src="images/Iconos/delete.png" style="cursor:pointer" data-toggle="tooltip" title="Eliminar" />'
        ]);
    });

    //*** 3.2 - Iniciar Tabla
    tabla = objtabla.DataTable({
        data: filas,
        "columns": [
            { "visible": false },
            { "width": "5%", "sortable": false, "className": "textoCentrado text-bold" },
            { "sortable": false },
            { "sortable": false },
            { "width": "5%", "sortable": false, "className": "textoCentrado" }
        ],
        "paging": false,
        "lengthChange": false,
        "searching": false,
        "info": true,
        "autoWidth": false,
        "destroy": true,
        "deferRender": true,
        "rowReorder": true,
        "scrollY": $(document).height() - 550 + "px",
        "scrollCollapse": true
    });

    tabla.draw();

    $('#tablaGestion tbody').on('click', 'tr', function () {
        if (!$(this).hasClass('selected')) {
            tabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
}
//*** (3.1) #4 - Tipo por Papaleta y Posición
function TipoPapeletaPosicion(Reincide, Num_paleta, Num_posicion) {

    switch (tipo) {

        //*** Delegados
        case 'D':
            return "<p style='margin:0px' data-idR='" + Reincide + "' data-idO='" + Num_paleta + "'>" + Num_paleta + "</p>";
        //*** Candidatos
        case 'C':
            return "<p style='margin:0px' data-idR='" + Reincide + "' data-idO='" + Num_posicion + "'>" + Num_posicion + "</p>";
    }
}
//*** (1.3.3) #5 - Cargar Papeletas
function obtenerPapeletas() {

    //*** 5.1 - Ajax Carga de Papeletas y Candidatos de la Papeleta X
    $.ajax({
        type: 'POST',
        async: true,
        url: apiUrl + '/api/Asamblea/PapeletaRonda/ListadoPapeletas',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Carga de Papeletas", mensajeError);
        },
        success: function (data) {

            if (data !== null && data.length > 0) {

                //*** Asignar Papeleta #1
                Id_papeleta = data[0].Id_papeleta;

                //*** Cambiar Título
                $('#lblGestion').html('USO DE LA PALABRA  ( <b class="text-danger"> ' + data[0].Id_papeleta + ' - ' + data[0].Nombre + ' </b>)');

                //*** 5.2 - Carga de Candidatos de la Papeleta
                CargarTablaGestionC(Id_papeleta);

                var listaPapeletas = [];

                $.each(data, function (index, val) {
                    var myCol = $('<span id="' + val.Id_papeleta + '" class="dropdown-item">' + val.Id_papeleta + ' - ' + val.Nombre + '</span><div class="dropdown-divider"></div>');
                    listaPapeletas.push(myCol);
                });

                $('#listaPapeletas').append(listaPapeletas);

                //*** Btn click Span dinámico
                $('.dropdown-item').click(function (e) {
                    //*** Evitar el PostBack
                    e.preventDefault();
                    //*** Variables
                    Id_papeleta = $(this).attr("id");
                    var texto = $(this).text();
                    //*** Cambiar Título
                    $('#lblGestion').html('USO DE LA PALABRA  ( <b class="text-danger"> ' + texto + ' </b>)');

                    //*** 5.3 - Carga de Candidatos de la Papeleta
                    CargarTablaGestionC(Id_papeleta);

                });
            }
        }
    });
}
//*** (1.6.2) #6 - Animación del Semaforo
function activarActivaDesactivaA(tipoA, reinc) {

    //*** Tipo de Registro ( Delegado  )
    if (tipo === 'D') {
        //*** Verifica si es Reincidente
        if (reinc === 'N') {

            if (tipoA === 'A') {
                $("#greenC").addClass("greenAnimation");
                $("#yellowC").addClass("yellowAnimation");
                $(".red").css("opacity", ".1");
            } else {
                $("#greenC").removeClass("greenAnimation");
                $("#yellowC").removeClass("yellowAnimation");
                $(".red").css("opacity", ".1");
            }
        } else {

            if (tipoA === 'A') {
                $("#greenC").addClass("greenAnimationR");
                $("#yellowC").addClass("yellowAnimationR");
                $(".red").css("opacity", ".1");
            } else {
                $("#greenC").removeClass("greenAnimationR");
                $("#yellowC").removeClass("yellowAnimationR");
                $(".red").css("opacity", ".1");
            }
        }
    } else {
        //*** Tipo de Registro ( Candidato )
        if (tipoA === 'A') {
            $("#greenC").addClass("greenAnimationC");
            $("#yellowC").addClass("yellowAnimationC");
            $(".red").css("opacity", ".1");
        } else {
            $("#greenC").removeClass("greenAnimationC");
            $("#yellowC").removeClass("yellowAnimationC");
            $(".red").css("opacity", ".1");
        }
    }
}
//*** (5.2 | 5.3) - #7 - Carga de Candadatos de la Papeleta X
function CargarTablaGestionC(id) {

    //*** 7.1 - Mostrar Cargando
    MostrarCargando();

    //*** 7.2 - Ajax Cargar Candidatos de Papeleta
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Gestion/ListaGestionC',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(id),
        dataType: 'JSON',
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Delegados en la gestion de la palabra", mensajeError);
            CerrarCargando();
        },
        success: function (data) {
            //*** 7.3 - Cargar Filas de Candidatos
            CargarFilas(data);
            //*** 7.4 - Cerrar Cargando
            CerrarCargando();
        }
    });
}
//*** (1.7.2) - #8 - Guarda registro de Delegado
function ActualizarGestion(tiempo) {

    //*** 8.1 - Mostrar Cargando
    MostrarCargando();

    DatosG.Num_paleta = idPaleta;
    DatosG.Cedula = cedulaO;
    DatosG.Usuario = login;
    DatosG.Tiempo = tiempo;

    //*** 8.2 - Ajax Ingresar Gestión de Delegado
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Gestion/ModificarGestion',
        data: JSON.stringify(DatosG),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': login + ':' + token
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaErrorMin(mensajeError);
            CerrarCargando();
        },
        success: function (data) {

            if (data.Error === 0) {

                alertaExitoMin('TIEMPO ALMACENADO CON EXITO.');
                //*** 8.3 - Cargar Delegados
                CargarTablaGestion();
                idPaleta = '0';
                paletaO = '0';

                //*** 8.4 - Cerrar Cargando
                CerrarCargando();

            } else {
                alertaErrorMin(data.Mensaje.toUpperCase());
                CerrarCargando();
            }
        }
    });
}
//*** (1.7.2) - #9 - Guarda registro de Candidato
function IngresarGestionC(tiempo) {
    //*** 9.1 - Mostrar Cargando
    MostrarCargando();

    DatosG.Num_posicion = paletaO;
    DatosG.Cedula = cedulaO;
    DatosG.Usuario = login;
    DatosG.Tiempo = tiempo;

     //*** 9.2 - Ajax Ingresar Gestión de Candidato
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Gestion/IngresarGestionC',
        data: JSON.stringify(DatosG),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': login + ':' + token
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaErrorMin(mensajeError);
            CerrarCargando();
        },
        success: function (data) {

            //*** 9.3 - Validar respuesta y Recargar datos
            if (data.Error === 0) {

                alertaExitoMin('TIEMPO ALMACENADO CON EXITO.');

                //*** 9.3.1 - Cargar Tabla de Candidatos
                CargarTablaGestionC(Id_papeleta);
                idPaleta = '0';
                paletaO = '0';
                //*** 9.3.2 - Cerrar Cargando
                CerrarCargando();

            } else {
                alertaErrorMin(data.Mensaje.toUpperCase());
                CerrarCargando();
            }
        }
    });
}
//*** (1.5.1) - #10 - Ingresar Delegado en la Lista ( Por # de Paleta)
function IngresarGestionDelegado() {

    //*** 10.1 - Mostar Cargando
    MostrarCargando();

    DatosG.Num_paleta = $("#numPapeleta").val();  
    DatosG.Usuario = login;

    //*** 10.2 - Ajax de Ingreso de Registro de Delegado
    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Gestion/IngresarGestion',
        data: JSON.stringify(DatosG),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': login + ':' + token
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaErrorMin(mensajeError);
            CerrarCargando();
        },
        success: function (data) {

            //*** 10.3 - Validar Resultado
            if (data.Error === 0) {

                alertaExitoMin(data.Mensaje.toUpperCase());

                //*** 10.3.1 - Recargar tabla de Delegados
                CargarTablaGestion();

                $("#numPapeleta").val('');

                CerrarCargando();
            } else {

                alertaErrorMin(data.Mensaje.toUpperCase());
                CerrarCargando();
            }
        }
    });
}
//*** (1.9.2.1) - #11 - Eliminar Delegado
function EliminarGestionDelegado(paleta) {
   
    if (idPaleta === paleta) {
        alertaError("Eliminar delegado", " No puede eliminar un delegado cuando el contador de tiempo ya se ha iniciado.");
        return;
    }

    MostrarCargando();

    DatosG.Num_paleta = paleta;
    DatosG.Cedula = cedulaO;
    DatosG.Usuario = login;


    $.ajax({
        type: 'POST',
        url: apiUrl + '/api/Asamblea/Gestion/EliminarGestionD',
        data: JSON.stringify(DatosG),
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        headers: {
            'Auth': login + ':' + token
        },
        error: function (xhr, ajaxOptions, thrownError) {
            mensajeError = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            alertaError("Eliminar delegado de la gestión:", mensajeError);
            CerrarCargando();
        },
        success: function (data) {

            if (data.Error === 0) {
                alertaExito('Uso de la Palabra', data.Mensaje.toUpperCase());
                CargarTablaGestion();
                CerrarCargando();
            } else {
                alertaError("Uso de la Palabra", data.Mensaje.toUpperCase());
                CerrarCargando();
            }
        }
    });
}

