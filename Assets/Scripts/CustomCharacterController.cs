using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CustomCharacterController : MonoBehaviour{

    [Header("Basic motion")]
	public Animator anim;
    public Rigidbody rig;
    public Transform mainCamera;
    public float jumpForce = 3.5f; 
    public float walkingSpeed = 2f;
    public float runningSpeed = 6f;
    public float currentSpeed;
    private float animationInterpolation = 1f;
    public FixedJoystick fixedJoystick;
    public float horizontal;
    public float vertical;
    public float lerpMultiplier = 7f;


    [Header("Сетевая синхронизация объектов")]
     public PhotonView view;
    public GameObject MainCamera;
    public GameObject CMVcam;
    public GameObject Joystick;
    public GameObject Interface;
    public CustomCharacterController scriptCustomCharacterController;

    private void Awake() 
    {
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
        {
        MainCamera.SetActive(false);
        CMVcam.SetActive(false);
        Joystick.SetActive(false);
        Interface.SetActive(false);
        scriptCustomCharacterController.enabled = false;
        }
    }

    void Start()
    {
        // Это короче чтоб курсор в центре экрана юлозился)
        //Cursor.lockState = CursorLockMode.Locked;
        // ну а это собсна невидимость курсора
        //Cursor.visible = false;
    }
    void Run()
    {
        animationInterpolation = Mathf.Lerp(animationInterpolation, 1.5f, Time.deltaTime * 3);
        anim.SetFloat("x", horizontal * animationInterpolation);
        anim.SetFloat("y", vertical * animationInterpolation);

        currentSpeed = Mathf.Lerp(currentSpeed, runningSpeed, Time.deltaTime * 3);
    }
    void Walk()
    {
        // Mathf.Lerp - отвечает за то, чтобы каждый кадр число animationInterpolation(в данном случае) приближалось к числу 1 со скоростью Time.deltaTime * 3. если я ниче не напутал)))
        // Time.deltaTime - это время между этим кадром и предыдущим кадром. Это позволяет плавно переходить с одного числа до второго НЕЗАВИСИМО ОТ КАДРОВ В СЕКУНДУ (FPS)!!!
        animationInterpolation = Mathf.Lerp(animationInterpolation, 1f, Time.deltaTime * 3);
        anim.SetFloat("x", horizontal * 1.5f);
        anim.SetFloat("y", vertical * 1.5f);

        currentSpeed = Mathf.Lerp(currentSpeed, runningSpeed, Time.deltaTime * 3);
    }
    private void Update()
    {
        horizontal = Mathf.Lerp(horizontal,fixedJoystick.Horizontal,Time.deltaTime * lerpMultiplier);
        vertical = Mathf.Lerp(vertical, fixedJoystick.Vertical, Time.deltaTime * lerpMultiplier); ;
        // Установил поворот персонажа когда камера поворачивается 
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,mainCamera.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        
        // Зажаты ли кнопки W и Shift?
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            // Зажаты ли еще кнопки A S D?
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                // Если да, то мы идем пешком
                Walk();
            }
            // Если нет, то тогда бежим!
            else
            {
                Run();
            }
        }
        // Если W & Shift не зажаты, то мы просто идем пешком
        else
        {
            Walk();
        }
        //Если зажат пробел, то в аниматоре отправляем сообщение тригеру, который активирует анимацию прыжка
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // Здесь задал движение персонажа в зависимости от направления в которое смотрит камера
        // и сохраняю направление вперед и вправо от камеры 
        Vector3 camF = mainCamera.forward;
        Vector3 camR = mainCamera.right;
        // Чтобы направления вперед и вправо не зависили от того смотрит ли камера вверх или вниз, иначе когда мы смотрим вперед, персонаж будет идти быстрее чем когда смотрит вверх или вниз
        // Можешь по приколу сам чекнуть убрав camF.y = 0 и camR.y = 0 :)
        camF.y = 0;
        camR.y = 0;
        Vector3 movingVector;
        // Тут умножаю нажатие на кнопки W & S на направление камеры вперед и прибавляем к нажатиям на кнопки A & D и умножаем на направление камеры вправо
        movingVector = Vector3.ClampMagnitude(camF.normalized * vertical * currentSpeed + camR.normalized * horizontal * currentSpeed,currentSpeed);
        // Magnitude - это длинна вектора. я делю длинну на currentSpeed так как мы умножаем этот вектор на currentSpeed на 86 строке. Я хочу получить число максимум 1.
        anim.SetFloat("magnitude", movingVector.magnitude/currentSpeed);
        Debug.Log(movingVector.magnitude / currentSpeed);
        // Здесь двигаю персонажа! Естессно установил движение только по x & z потому что мы не хотим чтобы наш персонаж взлетал в воздух:)
        rig.velocity = new Vector3(movingVector.x, rig.velocity.y,movingVector.z);
        // У меня был баг, что персонаж крутился на месте и это исправил с помощью этой строки
        rig.angularVelocity = Vector3.zero;
    }
    public void Jump()
    {
        // Выполняем прыжок по команде анимации.
        rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}