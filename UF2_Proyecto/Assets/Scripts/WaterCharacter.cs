using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCharacter : Character
{
    public Transform controladorAtk1;
    public Transform controladorAtk2;
    public Transform controladorAtk3;
    public Transform controladorSpecialAtk;
    public Transform controladorAirAtk;

    protected override void TimeInitializer(){
        TimeAtk1=0.3f;
        TimeAtk2=1.1f;
        TimeAtk3=1.0f;
        TimeSpecialAtk=2.8f;
    }

    protected override void MakeAtk1(){
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(controladorAtk1.position, new Vector2(1.5f, 0.1f), 0f);

        //foreach (Collider2D hit in hitEnemies)
        //{
        //    hit.GetComponent<Enemy>().RecibirDaño(10);
        //}
    }

    private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;

    // Define el centro y tamaño del rectángulo
    Vector3 center = controladorAtk1.position;
    Vector3 size = new Vector3(1.5f, 0.1f, 0f); // Puedes ajustar el tamaño según tus necesidades

    // Dibuja el rectángulo en el escenario
    Gizmos.DrawWireCube(center, size);
}

}
