using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // 총의 상태를 표현하는데 사용할 타입을 선언한다
    public enum State
    {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }

    public State state { get; private set; } // 현재 총의 상태

    public Transform fireTransform; // 총알이 발사될 위치

    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem shellEjectEffect; // 탄피 배출 효과

    private LineRenderer bulletLineRenderer; // 총알 궤적을 그리기 위한 렌더러

    private AudioSource gunAudioPlayer; // 총 소리 재생기
    public AudioClip shotClip; // 발사 소리
    public AudioClip reloadClipM; // 중간 재장전 소리
    public AudioClip reloadClipA; // 다 쓴 재장전 소리

    public float damage = 25; // 공격력
    private float fireDistance = 50f; // 사정거리

    public int ammoRemain = 100; // 남은 전체 탄약
    public int magCapacity = 25; // 탄창 용량
    public int magAmmo; // 현재 탄창에 남아있는 탄약


    public float timeBetFire = 0.12f; // 총알 발사 간격
    public float reloadTime = 1.8f; // 재장전 소요 시간
    private float lastFireTime; // 총을 마지막으로 발사한 시점


    private void Awake()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;

    }

    private void OnEnable()
    {
        // 총 상태 초기화
        //총알 가득 채우기
        magAmmo = magCapacity;
        // 총의 현재 상태를 발사 준비 상태로 변경
        state = State.Ready;
        // 마지막으로 총을 쏜 시점으로부터의 시간 0으로 초기화
        lastFireTime = 0;
    }

    // 발사 시도
    public void Fire()
    {
        //발사 가능한상태 && 마지막 발사로부터 timeBetFire 이상으로 지남
        if (state == State.Ready && Time.time >= lastFireTime + timeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
        }
    }

    // 실제 발사 처리
    private void Shot()
    {
        //레이캐스트 충돌정보 저장 컨테이너
        RaycastHit hit;
        // 탄알 맞은곳 변수
        Vector3 hitPosition = Vector3.zero;

        // 레이캐스트 시작지점,방향, 충돌정보 , 사정거리)
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            
            hitPosition = hit.point;
        }
        else
        {
            //충돌이 없었다면 최대 사거리의 위치를 충돌위치로 사용
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        StartCoroutine(ShotEffect(hitPosition));
        //남은탄창수 -1
        magAmmo--;
        if (magAmmo <= 0)
        {
            //탄창에 탄알이 없다면 상태를 Empty로 변경
            state = State.Empty;
        }


    }

    // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        //총구화염효과재생
        muzzleFlashEffect.Play();
        //탄피배출효과재생
        shellEjectEffect.Play();

        //소리재생
        gunAudioPlayer.PlayOneShot(shotClip);

        //선의 시작점은 총구의 위치
        bulletLineRenderer.SetPosition(0, fireTransform.position);

        //선의 끝점은 입력으로 들어온 충돌 위치
        bulletLineRenderer.SetPosition(1, hitPosition);



        //라인렌더러 활성화하여 궤적을 그림
        bulletLineRenderer.enabled = true;

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 총알 궤적을 지운다
        bulletLineRenderer.enabled = false;
    }

    // 재장전 시도
    public bool Reload()
    {
        if (state == State.Reloading || ammoRemain <= 0 || magAmmo >= magCapacity)
        {
            return false;
        }
        StartCoroutine(ReloadRoutine());

        return true;
    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine()
    {
        // 현재 상태를 재장전 중 상태로 전환
        state = State.Reloading;
        gunAudioPlayer.PlayOneShot(reloadClipM);
        // 재장전 소요 시간 만큼 처리를 쉬기
        yield return new WaitForSeconds(reloadTime);

        int ammoToFill = magCapacity - magAmmo;

        if (ammoRemain < ammoToFill)
        {
            ammoToFill = ammoRemain;
        }

        magAmmo += ammoToFill;
        ammoRemain -= ammoToFill;

        state = State.Ready;


        // 총의 현재 상태를 발사 준비된 상태로 변경
        state = State.Ready;
    }
}
