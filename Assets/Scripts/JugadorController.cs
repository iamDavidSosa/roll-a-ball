using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class JugadorController : MonoBehaviour {

	public float tiempoRestante = 60f;
	public Text textoTimer; 
	private bool juegoTerminado = false;
	private int totalColeccionables;
	private Rigidbody rb;
	private int contador;
	public Text textoContador, textoGanar;

	public float velocidad;
	void Start () {

		rb = GetComponent<Rigidbody>();

		contador = 0;

		totalColeccionables = GameObject.FindGameObjectsWithTag("Coleccionable").Length;
		setTextoContador(); 
		textoGanar.text = ""; 
	}
	void Update()
	{
		if (!juegoTerminado)
		{
			if (tiempoRestante > 0)
			{
				tiempoRestante -= Time.deltaTime;
				textoTimer.text = "Tiempo: " + Mathf.Floor(tiempoRestante).ToString();
			}
			else
			{
				Perder();
			}
		}
	}

	void Perder()
	{
		juegoTerminado = true;
		textoGanar.text = "¡Perdiste!";
		Invoke("VolverAlMenu", 5); 
	}

	void FixedUpdate () {

		float movimientoH = Input.GetAxis("Horizontal");
		float movimientoV = Input.GetAxis("Vertical");

		Vector3 movimiento = new Vector3(movimientoH, 0.0f,
			movimientoV);

		rb.AddForce(movimiento * velocidad);
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Coleccionable"))
		{
			other.gameObject.SetActive (false);

			contador = contador + 1;
			setTextoContador();
		}
	}

	void setTextoContador()
	{
		textoContador.text = "Contador: " + contador.ToString();
		if (contador >= totalColeccionables)
		{
			Ganar ();
		}
	}

	void Ganar()
	{
		textoGanar.text = "¡Ganaste!";
		Invoke("CargarSiguienteNivel", 2); 
	}

	void CargarSiguienteNivel()
	{
		int siguienteNivel = SceneManager.GetActiveScene().buildIndex + 1;
		SceneManager.LoadScene(siguienteNivel);
	}



}