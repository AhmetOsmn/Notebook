# KEYCLOAK REST API ILE OTURUM YÖNETIMI

## KULLANICI İÇİN ACCESS TOKEN ALMAK

- **[HTTP POST]**
- ENDPOINT: `{server-url}`/realms/`{realm}`/protocol/openid-connect/token
- REQUEST BODY (X-WWW-FORM-URLENCODED) (admin & master realm):
  - KEY:`grant_type` VALUE: **password**
  - KEY:`client_id` VALUE: `{client-id}`
  - KEY:`username` VALUE: `{username}`
  - KEY:`password` VALUE: `{password}`

- **[HTTP POST]**   
- REQUEST BODY (X-WWW-FORM-URLENCODED):
  - KEY:`grant_type` VALUE: **password**
  - KEY:`client_id` VALUE: `{client-id}`
  - KEY:`client_secret` VALUE: `{client-secret}`
  - KEY:`scope` VALUE: `{scope}`
  - KEY:`username` VALUE: `{username}`
  
## SERVİS İÇİN ACCESS TOKEN ALMAK

- **[HTTP POST]**
- ENDPOINT: `{server-url}`/realms/`{realm}`/protocol/openid-connect/token
- REQUEST BODY (X-WWW-FORM-URLENCODED):
  - KEY:`client_id` VALUE: `{client-id}`
  - KEY:`client_secret` VALUE: `{username}`
  - KEY:`grant_type` VALUE: **client_credentials**

## KULLANICILARI LİSTELEMEK

- **[HTTP GET]**
- BEARER TOKEN: `{TOKEN}`
- ENDPOINT: `{server-url}`/admin/realms/`{realm}`/users
- QUERY PARAMS:
  - briefRepresentation
  - email
  - first
  - firstName
  - lastName
  - max
  - search
  - username

## KULLANICININ KİMLİK BİLGİLERİNİ LİSTELEMEK

- **[HTTP GET]**
- BEARER TOKEN: `{TOKEN}`
- ENDPOINT: `{server-url}`/admin/realms/`{realm}`/users/`{userId}`/credentials

## KULLANICI OLUŞTURMAK

- **[HTTP POST]**
- BEARER TOKEN: `{TOKEN}`
- ENDPOINT: `{server-url}`/admin/realms/`{realm}`/users
- REQUEST BODY (JSON):
  ```json
  {
      "username": "ahmet",
      "enabled": true,
      "emailVerified": true,
      "firstName": "ahmet",
      "lastName": "sezgin",
      "email": "ahmet@gmail.com",
      "credentials": [
          {
              "type": "password",
              "value": "12345",
              "temporary": false
          }
      ]
      // daha fazla alanı da ekleyebiliyoruz.
  }
  ```
  
## KULLANICIYI GÜNCELLEMEK

- **[HTTP PUT]**
- BEARER TOKEN: `{TOKEN}`
- ENDPOINT: `{server-url}`/admin/realms/`{realm}`/users/`{userId}`
- REQUEST BODY (JSON):
  ```json
  {             
      "email": "osman@outlook.com"
      // daha fazla alanı da ekleyebiliyoruz.
  }
  ```

<br>
<br>
<br>
<br>
<br>

## KULLANICININ ŞİFRESİNİ SIFIRLAMAK

- **[HTTP PUT]**
- BEARER TOKEN: `{TOKEN}`
- ENDPOINT: `{server-url}`/admin/realms/`{realm}`/users/`{userId}`/reset-password
- REQUEST BODY (JSON):
  ```json
  {
      "type": "password",
      "temporary": false,
      "value": "123456"
  }
  ```

## KULLANICIYI SİLMEK

- **[HTTP DELETE]**
- BEARER TOKEN: `{TOKEN}`
- ENDPOINT: `{server-url}`/admin/realms/`{realm}`/users/`{userId}`

## ROLLERİ LİSTELEMEK

- **[HTTP GET]**
- BEARER TOKEN: `{TOKEN}`
- ENDPOINT: `{server-url}`/admin/realms/`{realm}`/clients/`{clientId}`/roles
- ENDPOINT: `{server-url}`/admin/realms/`{realm}`/roles

## ROL OLUŞTURMAK

- **[HTTP POST]**
- BEARER TOKEN: `{TOKEN}`
- ENDPOINT: `{server-url}`/admin/realms/`{realm}`/roles
- REQUEST BODY (JSON):
  ```json
  {
    "name": "Fighter",
    "composite": false,
    "clientRole": false,
    "containerId": "heroes"
  }
  ```

<br>
<br>
<br>
<br>
<br>
<br>
<br>

## ROL GÜNCELLEMEK

- **[HTTP PUT]**
- BEARER TOKEN: `{TOKEN}`
- ENDPOINT: `{server-url}`/admin/realms/`{realm}`/roles/`{roleName}`
- REQUEST BODY (JSON):
  ```json
  {
      "id": "ea522e2b-1e8f-42f6-9cc6-981fd5dd2114",
      "name": "Investigator",
      "composite": false,
      "clientRole": false,
      "containerId": "heroes",
      "attributes": {}
  }
  ```

## ROL SİLMEK

- **[HTTP DELETE]**
- BEARER TOKEN: `{TOKEN}`
- ENDPOINT: `{server-url}`/admin/realms/`{realm}`/roles/`{roleName}`

