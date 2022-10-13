var apiUrl = sessionStorage['Uri'];
var Login;

//*** #1 - Inicio
$(document).ready(function () {

     //*** Validar que la página no sea Votaciones.aspx
    if (window.location.pathname !== '/Votaciones.aspx') {
        //*** 1.1 - Cargar Menú
        AjaxCargarMenu(ocultarMenu);
    }

});
//*** (1.1) #2 - AjaxCargarMenú ( Menú  Dinámico)
function AjaxCargarMenu(CallbackFN) {

    //*** 2.1 - Variables del Proceso

    // Guardar la cédula en la variable Login
    Login = sessionStorage.getItem('Cedula');
    // Definir parametros del Ajax
    var data = new Object();
    data.Usuario = Login;
    // Url del WebApi
    var urlAjax = apiUrl + '/api/Usuario/CargarMenu';

    $.ajax({
        url: urlAjax,
        method: 'POST',
        data: JSON.stringify(data),
        async: true,
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        success: function (data) {

            //*** 2.2 - Validar si el usuario tiene acceso algún Menú 
            if (data.length === 0) {

                //*** 2.2.1 - CallBack de Acciones y control para desactivar Menú
                CallbackFN();

            } else {

                //*** 2.3 - Variables del Proceso

                // Mostrar el Menú
                mostrarMenu();

                // Contador para control del array "data"
                var cont = 0;
                // Lista de Menus
                var ListaMenu = new Array();
                // Lista de Modulos del array "data"
                var ListaModulos = ArrayValoresUnicos(data, "Modulo");

                //*** 2.4 - Ciclo del menú dinámico ( Modulos )
                for (var i = 0; i < ListaModulos.length; i++) {

                    //*** 2.5 - Variables del Ciclo FOR

                    // Nombre del Módulo 
                    var Modulo = ListaModulos[i];
                    // Html inicial del menú    ( 1 )
                    var Html_inicio_menu = $(data[cont].Html_inicio_menu);
                    // Html principal del menú  ( 2 )
                    var Html_menu = $(data[cont].Html_menu);
                    // Html inicial del submenu ( 3 )
                    var Html_inicio_submenu = $(data[cont].Html_inicio_submenu);

                    //*** 2.5 - Ciclo del menú dinámico ( SubModulos )
                    $.each(data, function (index, val) {
                        //*** 2.5.1 - SubModulos del Módulo X
                        if (Modulo === val.Modulo) {
                            
                            // SubMenu
                            var Html_submenu = $(val.Html_submenu);
                            // Agregar el SubMenu al Html de Inicio SubMenu
                            Html_submenu.appendTo(Html_inicio_submenu);
                            // Aumento del contador del array "data"
                            cont++;
                        }
                    });

                    //*** 2.6 - Agregar el contenido de los Subnodos dentro del Nodo Html_inicio_menu
                    Html_menu.appendTo(Html_inicio_menu);
                    Html_inicio_submenu.appendTo(Html_inicio_menu);

                    //*** 2.7 - Agregar Nodo completo a la lista
                    ListaMenu.push(Html_inicio_menu);
                }

                 //*** 2.8 - Agregar el Menú completo al tag con el id = menuPrincipal
                $('#menuPrincipal').append(ListaMenu);
            }
        },
        error: AjaxError
    });
}
//*** (2.1) #3 - ocultarMenu
function ocultarMenu() {
    $('#nav_menu').remove();
}
//*** (2.3) #4 - ocultarMenu
function ArrayValoresUnicos(array, columna) {

    return array.reduce((a, d) => {
        if (!a.includes(d[columna])) { a.push(d[columna]); }
        return a;
    }, []);
}
//*** (3.1) #5 - Eliminar Menú
function mostrarMenu() {

    $("body").addClass("sidebar-mini layout-navbar-fixed");
    $('.main-sidebar').css('visibility', 'visible');
    $('#nav_menu').css('visibility', 'visible');
}





