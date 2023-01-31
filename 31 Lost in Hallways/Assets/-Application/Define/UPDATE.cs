public enum UPDATE_CLASS {		Nothing,
								Scene,
								Character,		//캐릭터

								Environment,	//환경
								Motion,			//모션
                                Model,			//모델
								Sound,			//사운드
								Effect,			//이펙트
								Resource,		//리소스

								//Legacy
								Food,			//무기
								Ingredient,		//식재료
								Continent,		//대륙
								Region,			//지방
								Nation,			//국가
								City,			//도시
								Business,		//사업체
								Department,		//부서
								Product,		//상품
								Currency,		//통화
								Avatar,			//아바타

								Purchase,		//구매
								Help,			//도움말
								Charge,			//충전

                                End };

public enum UPDATE_TYPE {		Nothing,
								Bgsound,		//배경음악

								//Legacy
								//사업체 (BUSINESS)
								Farm,			//농장
								Factory,		//공장
								Market,			//소매점
								Pit,			//자원 채취
								Laboratory,		//연구소

								//상품 (PRODUCT)
								Crop,			//작물
								Animal,			//가축
								Agricultural,	//농축산물
								Natural,		//천연자원
								Retail,			//소비재
								Pakage,			//포장품

								//아바타 (AVATAR)
								Body,			//바디
								Face,			//얼굴
								Hair,			//헤어
								Kit,			//키트
								Backcolor,		//배경색

								/*
								//퀘스트 (QUEST)
								Day,			//일일
								Week,			//주간
								Achievement,	//업적
								Tutorial,		//튜토리얼
								*/

								//서비스 구매 (PURCHASE)
								Service,		//서비스
								Coin,			//코인

                                End };