# API 요청 방법

## 조회

### 계정 조회

###### URL
    /GetAccount
    
###### 인자
- `email` 이메일
- `name` 이름
- `password` 비밀번호
- `birthdate` 생년월일

###### 응답
```javascript
{
  "result": "OK",
  "rows": 2
  "managers": [
    {
      "email"     : "zetafie125@gmail.com", 
      "name"      : "정영우",
      "password"  : "098f6bcd4621d373cade4e832627b4f6",
      "birthdate" 1: "2003-02-28T00:00:00"
    }, ...
  ]
}
```

### 구단 조회

###### URL
    /GetTeam

###### 인자
- `name` 이름
- `location` 지역
- `title` 소개 제목
- `description` 소개 설명
- `color` 상징 컬러, 16진수 색상 코드
- `home` 홈구장
- `logo_url` 로고 url
- `grouppicture_url` 단체 사진 url
- `homepicture_url` 홈구장 사진 url

###### 응답
```javascript
{
  "result": "OK",
  "rows": 2
  "teams": [
    {
      "name"              : "주월FC",
      "location"          : "광주광역시 남구 주월동",
      "title"             : "주월동 동네 축구단",
      "description"       : "주월동우정사업본부공문서위조마스터 정영우가 운영하는 축구단",
      "color"             : "FFFF00",
      "home"              : "주월중학교 운동장",
      "logo_url"          : "http://juwol.gen.ms.kr/site/images/sub/sym_img01.jpg",
      "grouppicture_url"  : "https://www.gwangjufc.com/updata/edimg//XK9Lyi.JPG",
      "homepicture_url"   : "https://imgur.com/qdkwjqD.png"
    }, ...
  ]
}
```

### 감독 조회

###### URL
    /GetManager

###### 인자
- `account_email` 계정 이메일
- `team_name` 소속 구단 이름
- `picture_url` 프로필 사진 url

###### 응답
```javascript
{
  "result": "OK",
  "rows": 2
  "managers": [
    {
      "account_email" : "yys671@naver.com", 
      "team_name"     : "주월FC",
      "picture_url"   : "https://imgur.com/8F23sc9.png"
    }, ...
  ]
}
```

### 선수 조회

###### URL
    /GetPlayer

###### 인자
- `account_email` 계정 이메일
- `team_name` 소속 구단 이름
- `position` 포지션, 영어 축약어 사용
- `jersey_number` 등번호
- `height` 키
- `weight` 몸무게
- `is_captain` 기본값 0, 주장이면 1, 부주장이면 2
- `picture_url` 프로필 사진 url

###### 응답
```javascript
{
  "result": "OK",
  "rows": 2
  "managers": [
    {
      "account_email" : "zetafie125@gmail.com"
      "team_name"     : "주월FC"
      "position"      : "GK",
      "jersey_number" : 1,
      "height"        : 180,
      "weight"        : 110,
      "is_captain"    : 1,
      "picture_url"   : "https://demaras.files.wordpress.com/2018/08/like-a-boss.jpg"
    }, ...
  ]
}
```

## 등록 & 수정

###### 응답
```javascript
{
  "result": "OK",
  "rows_affected": 1
}
```

### 계정 등록 & 수정 

###### URL
    /ReplaceAccount

###### 인자
- `email` 필수
- `name` 필수
- `password` 필수
- `birthdate` 필수

### 구단 등록 & 수정

###### URL
    /ReplaceTeam

###### 인자
- `name` 필수
- `location` 필수
- `title` 필수
- `description` 필수
- `color` 필수
- `home`
- `logo_url` 필수
- `grouppicture_url` 필수
- `homepicture_url`

### 감독 등록 & 수정 

###### URL
    /ReplaceManager

###### 인자
- `account_email` 필수
- `team_name` 필수
- `picture_url` 필수

### 선수 등록 & 수정

###### URL
    /ReplacePlayer

###### 인자
- `account_email` 필수
- `team_name` 필수
- `position` 필수
- `jersey_number` 필수
- `height` 필수
- `weight` 필수
- `is_captain` 필수
- `picture_url` 필수

## 삭제

###### 응답
```javascript
{
  "result": "OK",
  "rows_affected": 1
}
```

### 계정 삭제

###### URL
    /DeleteAccount

###### 인자
- `email` 필수

### 구단 삭제

###### URL
    /DeleteTeam

###### 인자
- `name` 필수

### 감독 삭제

###### URL
    /DeleteManager

###### 인자
- `account_email` 필수

### 선수 삭제

###### URL
    /DeletePlayer

###### 인자
- `account_email` 필수
