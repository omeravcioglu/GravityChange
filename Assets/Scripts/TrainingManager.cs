using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    public Animator mainTrainingAnimator;

    void Start()
    {
        if (PlayerPrefs.HasKey("Training"))
        {
            deleteObjects();
        }

        StartCoroutine(trainingCoroutine());
    }

    private IEnumerator trainingCoroutine()
    {
        yield return new WaitUntil(() => GameManager.isGame);

        yield return new WaitForSeconds(1f);
        mainTrainingAnimator.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        mainTrainingAnimator.SetBool("toSuppress", true);
        yield return new WaitForSeconds(1f);
        mainTrainingAnimator.gameObject.SetActive(false);

        PlayerPrefs.SetInt("Training", 1);
        deleteObjects();
    }

    private void deleteObjects()
    {
        Destroy(mainTrainingAnimator.gameObject);
        Destroy(gameObject);
    }
}
