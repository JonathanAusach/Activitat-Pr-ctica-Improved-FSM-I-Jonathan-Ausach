using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorState2 : MonoBehaviour
{
    public static DoorState2 MyDoor;

    //stats

    State MyStat;

    //Stat 0 Open
    //Stat 1 Close
    //Stat 2 Opening
    //Stat 3 Closing

    [SerializeField] State[] Stats;

    //para hacer funcionar las funciones de los scriptobjects
    Controller MiController;

    //variables para conrtilar la puerta

    //Un Transform para mover las puertas
    [SerializeField] Transform door1;
    [SerializeField] Transform door2;

    [SerializeField] Button abrir;
    [SerializeField] Button cerrar;

    //Vectores 

    //cerrar
    Vector3 CerrarDoor1 = new Vector3(-3, 1, 0);
    Vector3 CerrarDoor2 = new Vector3(2, 1, 0);

    //Abrir
    Vector3 AbrirDoor1 = new Vector3(-6, 1, 0);
    Vector3 AbrirDoor2 = new Vector3(4, 1, 0);

    //activar el update para mover la puerta
    bool Up = false;

    //para saber si cerrar o abrir la puerta
    bool close;
    
    //velocidad de puerta, depende del estado seran velocidades diferentes
    float speed;
    private void Awake()
    {
        //sigleton
        if (MyDoor == null)
        {
            MyDoor = this;
        }
        else
        {
            Destroy(MyDoor.gameObject);
        }

        //Cerramos puerta para que no entren intrusos
        door1.position = CerrarDoor1;
        door2.position = CerrarDoor2;

        //establecemos el estado cerrado como preteminiado
        MyStat = Stats[1];
        ActiveAction();
        
    }

    private void Update()
    {
        //si up es true se abre o cierra la puerta
        if (Up)
        {
            //cierra la puerta
            if (close)
            {
                MoverDoor(door1, CerrarDoor1);
                MoverDoor(door2, CerrarDoor2);
            }
            //abre la puerta
            else
            {
                MoverDoor(door1, AbrirDoor1);
                MoverDoor(door2, AbrirDoor2);
            }
        }
    }

    //Funciones para los botones
    public void CerraDoor()
    {
        //cambiamos el estado segun el stat numero 1
        MyStat = Stats[1].transitions[0].CheckTransition(MiController);
        //Actvamos la opcion del estado
        ActiveAction();
    }

    public void AbrirDoor()
    {
        //cambiamos el estado segun el stat numero 0
        MyStat = Stats[0].transitions[0].CheckTransition(MiController);
        //Actvamos la opcion del estado
        ActiveAction();
    }

    //el estado abierto, se podra interuar con el boton cerrado, pero el abierto no se podra utilizar
    public void OpenDoor()
    {
        //desactivamos update
        Up = false;
        //desactvamos o activamos botnoes
        abrir.interactable = false;
        cerrar.interactable = true;
    }

    //lo mismo que el estado abierto, pero a la inversa
    public void CloseDoor()
    {
        //desactivamos update
        Up = false;
        //desactvamos o activamos botnoes
        cerrar.interactable = false;
        abrir.interactable = true;
    }

    //Estado que se esta abriendo la puerta, se llamara la funcion de mover puerta por cada puerta, para abrir
    public void OpeningDoor(float speedAux,bool closeAux)
    {
        //Activamos el update
        Up = true;
        //ponemos la velocidad y el bool del scriptobject
        speed = speedAux;
        close = closeAux;
    }

    //Estado que se cierra la puerta, se llama la funcion de mover puerta por cada puerta, para cerrar
    public void CloseingDoor(float speedAux, bool closeAux)
    {
        //Activamos el update
        Up = true;
        //ponemos la velocidad y el bool del scriptobject
        speed = speedAux;
        close = closeAux;
    }

    //funcion para cerara la puerta
    void MoverDoor(Transform Objecto, Vector3 objectivo)
    {
        //Movemos la puerta
        Objecto.position = Vector3.MoveTowards(
            Objecto.position,
            objectivo,
            speed * Time.deltaTime
         );

         //comprobamos que haya llegado a su objectivo
         if (Vector3.Distance(Objecto.position, objectivo) < 0.01f)
         {
            Objecto.position = objectivo;
            //Comprobamos si el estado es el num 2 o 3, saber si esta abriendo o cerrando la puerta en este caso
            if (MyStat == Stats[2])
            {
                MyStat = Stats[2].transitions[0].CheckTransition(MiController);
                ActiveAction();
            }
            else if (MyStat == Stats[3])
            {
                MyStat = Stats[3].transitions[0].CheckTransition(MiController);
                ActiveAction();
            }
         }
    }

    //funcion de activar la Accion del estado
    void ActiveAction()
    {
        MyStat.enterActions[0].Act(MiController);
        MyStat.actions[0].Act(MiController);
        MyStat.exitActions[0].Act(MiController);
    }
}
