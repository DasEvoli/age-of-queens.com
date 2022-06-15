# This Python script file is used to automatically update csv files we use on the website
# Reason for this is that this makes it very easy for users to update the data by themselves

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
    url_leaderboard = 'https://docs.google.com/spreadsheets/d/1mUtPPjgq07ibmVdhXrpHcdEYurK9OB22_1wdLdYHO-E/export?format=csv&id=1mUtPPjgq07ibmVdhXrpHcdEYurK9OB22_1wdLdYHO-E&gid=121628420'
    download_csv(url_leaderboard, 'leaderboard_rm.csv')
    time.sleep(TIMEOUT)
