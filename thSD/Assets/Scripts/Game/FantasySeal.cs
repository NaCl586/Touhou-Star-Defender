﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FantasySeal : MonoBehaviour
{
    public char direction = 'r';
    public Fairy lockedFairy;
    private GameManager _gm;

    public static List<Fairy> selectedFairy = new List<Fairy>();

    private int phase = 0;

    void OnEnable()
    {
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void prepareShot()
    {
        if (_gm.fairies.Count == 0 || _gm.fairies.Count == selectedFairy.Count)
        {
            Destroy(gameObject);
            return;
        }

        do
        {
            lockedFairy = _gm.fairies[Random.Range(0, _gm.fairies.Count)];
        }
        while (selectedFairy.Contains(lockedFairy));
        selectedFairy.Add(lockedFairy);

        if (phase == 0)
        {
            if (direction == 'r')
                transform.DOMove(transform.position + Vector3.right, 0.75f).OnComplete(() => { phase = 1; });
            else if (direction == 'l')
                transform.DOMove(transform.position + Vector3.left, 0.75f).OnComplete(() => { phase = 1; });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(phase == 1)
        {
            Vector3 dir = (lockedFairy.transform.position - transform.position).normalized;
            transform.position += dir * 0.01f;

            if(Vector3.Distance(lockedFairy.transform.position, transform.position) < 0.1)
            {
                lockedFairy.FairyDeath();
                Destroy(gameObject);
                return;
            }
        }
    }
}