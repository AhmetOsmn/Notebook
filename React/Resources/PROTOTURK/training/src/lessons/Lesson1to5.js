import '../tailwind.css';

import { useState } from 'react';
import Button from '../components/Button';
import Tab from '../components/Tab';


function FirstFiveLessons() {
    const [activeTab, setActiveTab] = useState(1)

    return (
        <>
      <div className="p-2">
        <Button setActiveTab={setActiveTab} text="3.yü aktif et" variant='warning'/>
        <Tab activeTab={activeTab} setActiveTab={setActiveTab}>
          <Tab.Panel title="Profil">1. tab</Tab.Panel>
          <Tab.Panel title="Hakkında">2. tab</Tab.Panel>
          <Tab.Panel title="Ayarlar">3. tab</Tab.Panel>
        </Tab>
      </div>
      <div className="p-2">
        <Button text="Buton örneği" />
        <Button text="Buton örneği" variant='success' />
        <Button text="Buton örneği" variant='danger' />
        <Button text="Buton örneği" variant='warning' />
      </div>
    </>
    );
}

export default FirstFiveLessons;