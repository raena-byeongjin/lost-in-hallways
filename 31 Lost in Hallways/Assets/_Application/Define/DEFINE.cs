//#define WEB_VIEW						웹뷰
//#define SIMPLE_FILE_BROWSER			파일 뷰어 (에디터 용)
//#define AUP							안드로이트 네이티브 플러그인
//#define ADMOB							구글 애드

public enum SCENE {                     NOTHING,
										APPLICATION_CONTRACT,		// 1: 앱 이용 계약
										SAFE_CLASS,					// 2: 안전 교실
										PLAY,						// 3: 플레이

                                        END };

public enum VIEW {                      NOTHING,

                                        END };

public enum ALIGN {                     NOTHING,
                                        LEFT,
                                        TOP,
                                        RIGHT,
                                        BOTTOM,
                                        CENTER,
                                        LEFT_TOP,
                                        RIGHT_TOP,
                                        LEFT_BOTTOM,
                                        RIGHT_BOTTOM,

                                        END };

public enum CONFIRM {					NOTHING,
										OK,
										YESNO,

                                        END };

public enum DIRECTION {                 RIGHT,
                                        RIGHT_UP,
                                        UP,
                                        LEFT_UP,
                                        LEFT,
                                        LEFT_DOWN,
                                        DOWN,
                                        RIGHT_DOWN,

                                        END };
//CLIENT_MESSAGE
public enum CLIENT_MESSAGE {		    NOTHING,
										ERROR,							//ERROR
										LOGIN_OK,						//로그인에 성공했습니다.
										LOGIN_PASSWORD_ERROR,			//비밀번호가 틀렸습니다.
										LOGIN_OVERLAP,					//다른곳에서 로그인 되어 있습니다.
										LOGIN_ID_NOT_FOUND,				//아이디를 찾을 수 없습니다.
										DISCONNECT,						//연결이 해제되었습니다.

										FORCED_OUT,						//강퇴되었습니다.
										FORCED_ROOM_FULL,				//방이 가득참
										DICE_START_PEOPLE_NOT,			//2명 이상이어야 시작할 수 있습니다.
										GP_SHORT,						//GP가 부족합니다.
										ROOM_OUT_5MINUTE_LIMIT,			//방 입장 후, 5분 이내에 다시 방에 입장할 수 없습니다.

										IS_ALREADY_CHAT_OTHER_USERS,	//이미 다른 유저와 대화중입니다.

										END };

//W_MESSAGE
public enum WM {		                NOTHING,
										QUIT,
										CONFIRM,
										CALL,

										END };

public enum ENABLE_N {					NOTHING,
										AWAKE,
										START,
										ENABLE,

										END };

public enum DAY {						NOTHING,
										NIGHT,

										END };

//취소 입력
public enum CANCEL {					NOTHING,
										ESCAPE,
										BACKSPACE,
										MOUSE_RBUTTON,
										CLOSE,
										BUTTTON,
										NETWORK,
										PAD_X,
										HISTORY_BACK,

										END };

public enum ANIMATION_FRAME {			NOTHING,
										OVER,
										REPEAT,
										HIT,

										END };

public enum PLAYER_SEARCH_SORT {		LATEST,		//최근 순
										NEAR,		//거리 순

										END };

public enum PLAYER_SEARCH_RANGE {		WITHIN_24HOURS,		//24시간 이내
										WITHIN_3DAYS,		//3일 이내
										WITHIN_7DAYS,		//7일 이내
										WITHIN_30DAYS,		//30일 이내

										END };

public enum ACTION_TYPE {				NOTHING,
										WALK,

										END };