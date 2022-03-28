import time
import requests
import os

DIR_PATH = os.path.dirname(os.path.realpath(__file__))
SAVE_PATH = '/Csv/'
TIMEOUT = 600

def download_csv(url :str, filename :str):
    try:
        r = requests.get(url, allow_redirects=True)
        if(r.status_code == 200):
            open(DIR_PATH + SAVE_PATH + filename, 'wb').write(r.content)
            r.close()
        else:
            raise Exception("Request Status Code %s", r.status_code)
    except Exception as e:
        print(e)
    finally:
        r.close()

while True:
    url_casting_roster = 'https://docs.google.com/spreadsheets/d/1zmYGhrCTQN3L5WDFfAYkThcuJRKOMLKE2MOvBykbT7I/export?format=csv&id=1zmYGhrCTQN3L5WDFfAYkThcuJRKOMLKE2MOvBykbT7I&gid=0'
    url_solo_seed = 'https://docs.google.com/spreadsheets/d/1ALlkjl-jcCAHdapkVypjhUGkfyaNf3ebIUUw9F6APE8/export?format=csv&id=1ALlkjl-jcCAHdapkVypjhUGkfyaNf3ebIUUw9F6APE8&gid=0'
    url_leaderboard = 'https://docs.google.com/spreadsheets/d/1mUtPPjgq07ibmVdhXrpHcdEYurK9OB22_1wdLdYHO-E/export?format=csv&id=1mUtPPjgq07ibmVdhXrpHcdEYurK9OB22_1wdLdYHO-E&gid=121628420'
    url_blogposts = 'https://docs.google.com/spreadsheets/u/1/d/1CN9ztxk-2IHs5SfhSGHKWRU0oULHVVe3GdkOjAwwkIM/export?format=csv&id=1CN9ztxk-2IHs5SfhSGHKWRU0oULHVVe3GdkOjAwwkIM&gid=1046398781'
    url_teamseed = 'https://docs.google.com/spreadsheets/d/1Ljr1248kdWjpxuOuSE_Y-mDNLUGn00zoBehrStFhsnQ/export?format=csv&id=1Ljr1248kdWjpxuOuSE_Y-mDNLUGn00zoBehrStFhsnQ&gid=0'
    url_mods = 'https://docs.google.com/spreadsheets/d/1lxuqnQGlkopeHeKBp8qDMGgFC72Cy5ZGWBA5kks-VBc/export?format=csv&id=1lxuqnQGlkopeHeKBp8qDMGgFC72Cy5ZGWBA5kks-VBc&gid=0'
    url_currentevent = 'https://docs.google.com/spreadsheets/d/1ro9r0h6xNabPvelKh7pviK682tTQE_bLCBrvXkta_Hc/export?format=csv&id=1ro9r0h6xNabPvelKh7pviK682tTQE_bLCBrvXkta_Hc&gid=1420222812'
    download_csv(url_casting_roster, 'casting_roster.csv')
    download_csv(url_solo_seed, 'solo_seed.csv')
    download_csv(url_leaderboard, 'leaderboard_rm.csv')
    download_csv(url_blogposts, 'blog_posts.csv')
    download_csv(url_teamseed, 'team_seed.csv')
    download_csv(url_mods, 'mods.csv')
    download_csv(url_currentevent, 'current_event_data.csv')
    time.sleep(TIMEOUT)
