using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int currentExe = 1;
    private Action done = null;

    [SerializeField] ExePlayer player;

    [Header("Label Log:")]
    [SerializeField] Text textLabel;
    [SerializeField] Text textConsole;

    [Header("Points:")]
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    [SerializeField] GameObject pointC;

    private Transform pA;
    private Transform pB;
    private Transform pC;

    private float movingTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        // Exercise 2
        pointA.SetActive(false);
        pointB.SetActive(false);
        pointC.SetActive(false);

        SetLabel("Thanh Bui's Exercise");
        SetConsole("");

        pA = pointA.transform;
        pB = pointB.transform;
        pC = pointC.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            switch (currentExe)
            {
                case 1:
                    {
                        SetConsole("Input Key Down A");
                        Invoke(nameof(ResetBlankConsole), 3f);
                        break;
                    }
                case 2:
                    {
                        SettingExe();
                        SettingExe();
                        SetLabel("Exercise 2 - Transform.Translate Move");

                        TranslateMove moveMethod = new(done, pA, pB, pA);
                        player.SetMove(moveMethod);
                        break;
                    }

            }

        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            switch (currentExe)
            {
                case 1:
                    {
                        SetConsole("Input Key Down B");
                        Invoke(nameof(ResetBlankConsole), 3f);
                        break;
                    }
                case 2:
                    {
                        SettingExe();
                        SetLabel("Exercise 2 - Rigidbody.velocity Move");

                        VelocityMove velocityMove = new(done, pA, pB, pA);
                        player.SetMove(velocityMove);

                        break;
                    }
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            switch (currentExe)
            {
                case 1:
                    {
                        SetConsole("Input Key Down C");
                        Invoke(nameof(ResetBlankConsole), 3f);
                        break;
                    }
                case 2:
                    {
                        SettingExe();
                        SetLabel("Exercise 2 - Vector3.MoveToWards Move");

                        MoveToWards moveToWardsMove = new(done, pA, pB, pA);
                        player.SetMove(moveToWardsMove);

                        break;
                    }
            }
        }

        // Exercise 1
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentExe = 1;
            string label = "Exercise 1";
            SetLabel(label);
            ResetBlankConsole();
        }


        // Exercise 2
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentExe = 2;
            SettingExe();

            string console = "A - Transform.Translate\n" +
                "B - Rigidbody.velocity\n" +
                "C - Vector3.MoveToWards";
            SetConsole(console);
        }

        // Exercise 3
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentExe = 3;

            SettingExe();

            string console = "Moving Type: Transform.Translate\n"
                + "Route: Loop(Point A -> Point B -> Point C)";
            SetConsole(console);

            TranslateMove moveMethod = new(done, pA, pB, pC)
            {
                IsLoop = true
            };
            player.SetMove(moveMethod);

        }

        // Exercise 4
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentExe = 4;

            SettingExe();

            LerpMove lerpMove = new(done, pA, pB, pC)
            {
                IsLoop = true
            };
            player.SetMove(lerpMove);

        }

        // Exercise 6
        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentExe = 6;

            SettingExe();
        }

        if (currentExe == 6)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                Vector2 offset = new Vector2(horizontal, vertical);
                player.transform.Translate(player.speed * Time.deltaTime * offset);
            }
        }

        // Exercise 7
        if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7))
        {
            currentExe = 7;

            SettingExe();

            TranslateMove moveMethod = new(done, pA, pB, pC)
            {
                IsRandomRoute = true
            };
            player.SetMove(moveMethod);

        }

        // Exercise 8
        if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8))
        {
            currentExe = 8;

            SettingExe();

            TranslateMove moveMethod = new(done, pA, pB, pC)
            {
                IsRandomRoute = true
            };
            player.SetMove(moveMethod);

        }

        if (currentExe == 8)
        {
            movingTime += Time.deltaTime;

            int round = Mathf.RoundToInt(movingTime);
            player.moveMethod.IsSleep = round % 2 == 1;
        }

    }

    private void SettingExe()
    {
        player.moveMethod?.MoveDone(player);

        SetLabel("Exercise " + currentExe);
        ResetBlankConsole();

        switch (currentExe)
        {
            case 2:
                {
                    pointA.SetActive(true);
                    pointB.SetActive(true);

                    done = () =>
                    {
                        pointA.SetActive(false);
                        pointB.SetActive(false);
                        SetLabel("Exercise " + currentExe);
                    };
                    break;
                }
            case 3:
            case 4:
            case 7:
            case 8:
                {
                    player.rb.bodyType = RigidbodyType2D.Kinematic;

                    pointA.SetActive(true);
                    pointB.SetActive(true);
                    pointC.SetActive(true);

                    done = () =>
                    {
                        pointA.SetActive(false);
                        pointB.SetActive(false);
                        pointC.SetActive(false);
                        SetLabel("Exercise " + currentExe);
                    };
                    break;
                }
            case 6:
                {
                    player.rb.bodyType = RigidbodyType2D.Kinematic;

                    break;
                }
            

        }
    }

    private void SetLabel(string label)
    {
        textLabel.text = label;
    }

    private void SetConsole(string console)
    {
        textConsole.text = console;
    }

    private void ResetBlankConsole()
    {
        textConsole.text = "";
    }
}
