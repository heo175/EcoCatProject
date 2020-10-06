# 가천대학교 컴퓨터공학과 2019 졸업프로젝트
환경지키미 에코냥 - Hingya팀

- 팀장 - 허윤신 (Unity, c#, SQLite) @heo175
- 팀원 - 이선민 (Unity, c#, firebase, UX Design) @ji_ji
- 팀원 - 김채원 (UI Design, Graphic Design)

## 1. 배경

환경오염이란 인간 생활이나 생산 소비의 과정에서 배출되는 물질들이 생활환경을 오염시켜 인간과 생물의 건강과 생존에 장애를 주는 현상을 의미한다. 최근 심각한 환경오염으로 인해 환경 보호에 대한 관심이 대두되고 있다. 유년기부터 환경문제를 인식하고, 해결과정에 참여할 수 있도록 환경교육을 실시해야한다고 생각한다. 
이 시스템은 스토리텔링 기반의 모바일 게임으로, 진행되는 이야기에 따라 환경오염의 심각성과 원인을 인식하고, 이에 대한 해결방안과 예방법을 배울 수 있도록 하는 것을 목적으로 기획하였다. 또한 다양한 게임과 기능들을 통해 자연스럽게 분리배출과 환경 정보를 알 수 있도록 기획하였다. 
 
## 2. 주요 기능
  - 튜토리얼
  게임을 진행시키는 캐릭터 에코냥과의 첫 만남 튜토리얼이 게임의 시작과 함께 진행. 각 동물들이 위험에 처하게 된 환경오염의 원인과 해결 방안을 제시하며 게임의 이야기 전개.
  - 미니게임
  분리배출, 쓰레기 줄이기, 나무 키우기 등의 환경 교육 게임. 환경에 대한 관심을 높이고, 분리배출의 지식을 향상시킬 수 있도록 주제를 기획.
  - 걸음수 측정
  가속도계 센서를 이용한 걸음수 측정. 일주일간의 걸음 기록 그래프 확인 가능.
  - 뷰포리아
  뷰포리아의 이미지 타켓팅을 통해 분리배출표시제도 마크 인식. 인식된 마크에 따라 분리배출 방법 설명.
  - 환경 정보
  어린이를 대상으로 환경에 대한 지식을 쉽게 풀어 설명.
  

## 3. 적용 기술
### 1. Vuforia
 Vuforia는 교차 플랫폼 증강 현실(AR) 및 혼합 현실(MR) 애플리케이션 개발 플랫폼이다. 모바일 기기, Microsoft HoloLens와 같은 혼합 현실 헤드 마운트 디스플레이 등과 같은 다양한 하드웨어에서 뛰어난 추적 기능과 성능을 제공한다. 
 Vuforia의 기능 중 마커 기반 추적을 이용한다. AR 또는 MR에서 마커는 애플리케이션에 등록된 이미지 또는 오브젝트로, 애플리케이션에서 정보 트리거 역할을 한다. 
 마커 기반 추적의 마커 타입으로 이미지 타켓을 사용한다. 이미지 타겟은 마커 기반 추적에 사용되는 특정 마커 타입으로, 애플리케이션에서 수동으로 등록해야 하고, 가상 콘텐츠를 표시하는 트리거 역할을 하는 이미지이다. 

### 2. Android Motion Sensor

 안드로이드에서 제공하는 Motion Sensor API 중, 가속도계 센서를 이용한다. 가속도계 센서는 3차원 공간에서의 방향과 움직임에 대한 값을 알려준다. 가속도계 센서는 중력의 영향을 받으며, 디바이스의 각도가 3차원 공간의 축과 틀어지면 값이 증감한다.

### 3. Firebase Unity SDK

 Firebase Unity SDK 중 FirebaseAuth.unitypackage를 이용해 앱에 Google 로그인을 통합하여 사용자가 Google 계정을 통해 Firebase에 인증한다. Firebase 실시간 데이터베이스와 Cloud Storage 보안 규칙에서 로그인한 사용자의 고유 사용자 ID를 통해 사용자가 액세스할 수 있는 데이터를 관리할 수 있다.

### 4. Google Play 게임 서비스

  Google Play 게임 서비스를 사용하여 Firebase를 기반으로 개발된 Android 게임에 플레이어가 로그인하도록 할 수 있다. Google Play 게임에 플레이어를 로그인 처리하면서 OAuth 2.0 인증 코드를 요청하고, 인증 코드를 전달하여 Firebase 사용자 인증 정보를 생성한다. 이 인증 정보를 사용하여 Firebase에 인증할 수 있다.
  
## 4. 스토리 진행
![KakaoTalk_20201006_125153915](https://user-images.githubusercontent.com/46212602/95157214-54f88880-07d3-11eb-96fa-463dcfc15d1a.jpg)
![KakaoTalk_20201006_125153915_01](https://user-images.githubusercontent.com/46212602/95157216-5629b580-07d3-11eb-90f7-cb5bf50784fc.jpg)
![KakaoTalk_20201006_125153915_02](https://user-images.githubusercontent.com/46212602/95157218-56c24c00-07d3-11eb-988f-4663f6f57c64.jpg)
![KakaoTalk_20201006_125153915_03](https://user-images.githubusercontent.com/46212602/95157220-575ae280-07d3-11eb-85ae-a3176547496e.jpg)

 환경을 지키는 고양이 에코냥이 숲 속에 쓰러져 있는 것을 발견하고, 앞으로 에코냥과 함께 위기에 처한 동물들을 도와주기로 한다. 지구온난화로 인해 빙하가 녹아 엄마를 잃어버린 북극곰, 바다 속 플라스틱을 먹은 가족들로 울고 있는 돌고래, 지구온난화로 대나무가 사라진 팬더, 자동차 배기가스로 고통받는 나무의 순서로 이야기가 진행된다. 각 동물 스토리는 인트로가 먼저 진행되며, 인트로가 끝나면 에너지를 사용해 도와주기를 5단계 마쳐야한다. 5단계를 마친 후, 아웃트로가 등장한다. 
 
## 5. 실행과정

### 1. 구글 계정 인증 및 튜토리얼 진행
![KakaoTalk_20201006_121451997](https://user-images.githubusercontent.com/46212602/95154865-c9302d80-07cd-11eb-863d-052f8fc41c97.jpg)
![KakaoTalk_20201006_121451997_01](https://user-images.githubusercontent.com/46212602/95154870-c9c8c400-07cd-11eb-982b-e49ed9685cc8.jpg)
![KakaoTalk_20201006_121451997_02](https://user-images.githubusercontent.com/46212602/95154873-ca615a80-07cd-11eb-99b9-c3214d658059.jpg)

Play game이 설치되어 있고, 구글 계정이 로그인된 상태라면 환경지키미 에코냥 애플리케이션을 켰을 때 구글 계정 인증을 한다. 인증에 성공할시, 닉네임을 입력받음과 동시에 회원가입이 된다. 에코냥과의 만남 튜토리얼로 이어지면서 게임의 이야기가 진행된다. 
 
### 2. 동물 도와주기
![KakaoTalk_20201006_123917846](https://user-images.githubusercontent.com/46212602/95157080-0519c180-07d3-11eb-8656-1d2782911331.jpg)
![KakaoTalk_20201006_123917846_01](https://user-images.githubusercontent.com/46212602/95157083-05b25800-07d3-11eb-9d02-0c3b53c173c0.jpg)
![KakaoTalk_20201006_123917846_02](https://user-images.githubusercontent.com/46212602/95157084-05b25800-07d3-11eb-925d-635e5605b4c6.jpg)

에너지를 사용해 동물을 도와줄 수 있다. 각 동물들은 다섯 개의 단계를 가지고 있으며, 단계가 진행될수록 사용 에너지가 2000씩 늘어난다. 에너지를 사용하면 보이는 메인 페이지의 그래픽이 달라진다. 단계별로 개선되는 동물의 환경을 볼 수 있다.


### 3. 미니게임
#### 1. 캐치캐치 쓰레기받기
![KakaoTalk_20201006_110600984](https://user-images.githubusercontent.com/46212602/95151880-bebe6580-07c6-11eb-8470-2c45e3f07f65.jpg)
![KakaoTalk_20201006_110600984_01](https://user-images.githubusercontent.com/46212602/95151881-bf56fc00-07c6-11eb-8e2f-6e111e8a1f48.jpg)
![KakaoTalk_20201006_110600984_02](https://user-images.githubusercontent.com/46212602/95151883-bfef9280-07c6-11eb-83db-61330e58a63c.jpg)
![KakaoTalk_20201006_110600984_03](https://user-images.githubusercontent.com/46212602/95151884-bfef9280-07c6-11eb-8566-5b25bfa59ea7.jpg)

우측하단에 있는 분리배출표시제도 마크에 따라 하늘에서 내려오는 쓰레기를 양동이로 받는 게임. 하트를 받으면 생명이 추가되고, 마크에 맞지 않은 쓰레기를 받으면 생명이 감소한다. 받아야 할 목표 쓰레기 개수에 맞게 모두 받으면 게임 성공. 생명이 모두 깎이거나, 시간이 0이 되면 게임 실패.

#### 2. 나무 키우기 게임
![KakaoTalk_20201006_114946794](https://user-images.githubusercontent.com/46212602/95154147-29be6b00-07cc-11eb-9504-b79d424aec75.jpg)
![KakaoTalk_20201006_114946794_01](https://user-images.githubusercontent.com/46212602/95154150-2a570180-07cc-11eb-965a-f7b1192f5a1b.jpg)
![KakaoTalk_20201006_114946794_02](https://user-images.githubusercontent.com/46212602/95154151-2aef9800-07cc-11eb-8071-ab5d0adcfe24.jpg)
![KakaoTalk_20201006_114946794_03](https://user-images.githubusercontent.com/46212602/95154152-2aef9800-07cc-11eb-9b7d-ddffe8b30431.jpg)

설명에 따라 삽, 씨앗, 물, 햇빛, 잡초 순으로 빠르게 클릭해 나무를 키우는 게임. 제한 시간 안에 나무를 키우면 게임 성공. 시간이 지나면 게임 실패.

#### 3. 쓰레기를 없애라!
![KakaoTalk_20201006_115811953](https://user-images.githubusercontent.com/46212602/95154382-abae9400-07cc-11eb-860f-47dc629a18b5.jpg)
![KakaoTalk_20201006_115811953_01](https://user-images.githubusercontent.com/46212602/95154384-ac472a80-07cc-11eb-88ce-2c77290a552c.jpg)
![KakaoTalk_20201006_115811953_02](https://user-images.githubusercontent.com/46212602/95154386-ac472a80-07cc-11eb-80d7-4532821c7360.jpg)
![KakaoTalk_20201006_115811953_03](https://user-images.githubusercontent.com/46212602/95154387-acdfc100-07cc-11eb-8e8c-30f7d4c4e641.jpg)

제한 시간 안에 쓰레기를 터치해 모두 없애는 게임. 시간 안에 모든 쓰레기를 없애면 게임 성공. 시간이 0이 되면 게임 실패.

#### 4. 환경 OX 퀴즈
![KakaoTalk_20201006_115756435](https://user-images.githubusercontent.com/46212602/95154443-cbde5300-07cc-11eb-9fe6-d8c2a8808054.jpg)
![KakaoTalk_20201006_115756435_01](https://user-images.githubusercontent.com/46212602/95154445-cc76e980-07cc-11eb-8bad-caf47158ea12.jpg)
![KakaoTalk_20201006_115756435_02](https://user-images.githubusercontent.com/46212602/95154446-cc76e980-07cc-11eb-9cd0-411e99644b5c.jpg)
![KakaoTalk_20201006_115756435_03](https://user-images.githubusercontent.com/46212602/95154447-cd0f8000-07cc-11eb-820b-d29be9b0c244.jpg)

환경과 관련된 문제를 읽고 O, X로 답을 맞추는 게임. 환경 정보 페이지의 내용으로 문제 구성. 틀리면 생명이 감소하며, 제한 시간 안에 목표 문제 개수를 맞춰야 게임 성공. 생명이 0이 되거나, 시간 안에 문제를 맞추지 못 하면 게임 실패. 

#### 5. 에코런
![KakaoTalk_20201006_115734169](https://user-images.githubusercontent.com/46212602/95154494-e9132180-07cc-11eb-8ad4-c7180f66fc4c.jpg)
![KakaoTalk_20201006_115734169_01](https://user-images.githubusercontent.com/46212602/95154496-ea444e80-07cc-11eb-846a-ba0908ed74da.jpg)
![KakaoTalk_20201006_115734169_02](https://user-images.githubusercontent.com/46212602/95154497-ea444e80-07cc-11eb-803e-c29f7e0a6577.jpg)
![KakaoTalk_20201006_115734169_03](https://user-images.githubusercontent.com/46212602/95154498-eadce500-07cc-11eb-816c-31e5db8b198b.jpg)

새싹을 밟지 않고 목표 쓰레기 개수를 맞춰 성에 도착하는 게임. 하트를 먹으면 생명이 증가하고, 새싹을 밟으면 생명이 감소한다. 목표 쓰레기 개수를 채우면 게임 성공. 생명이 다 사라지면 게임 실패.

#### 6. 점핑! 분리배출
![KakaoTalk_20201006_115718401](https://user-images.githubusercontent.com/46212602/95154577-1790fc80-07cd-11eb-91ea-451d1659fff9.jpg)
![KakaoTalk_20201006_115718401_01](https://user-images.githubusercontent.com/46212602/95154579-18299300-07cd-11eb-8284-73b91e166f70.jpg)
![KakaoTalk_20201006_115718401_02](https://user-images.githubusercontent.com/46212602/95154580-18299300-07cd-11eb-948d-0db653ca47fc.jpg)
![KakaoTalk_20201006_115718401_03](https://user-images.githubusercontent.com/46212602/95154581-18c22980-07cd-11eb-8f8f-478bca3c23b9.jpg)

캔, 종이, 플라스틱 중 랜덤으로 나오는 좌측하단의 쓰레기의 종류에 맞게 발판을 밟고 올라가 알맞은 쓰레기통에 도착하는 게임. 알맞은 쓰레기통에 도착하면 게임 성공. 쓰레기 종류에 맞지 않는 쓰레기통에 도착하면 게임 실패. 
