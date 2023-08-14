import { useEffect, useState } from "react";
import Button from "../components/Button";
import Header from "../components/Header";
import Card from "../components/Card";


function Lesson10() {

    const genders = [
        { key: "1", value: "Erkek" },
        { key: "2", value: "Kadın" }
    ]

    const categoryList = [
        { key: 1, value: 'CSharp' },
        { key: 2, value: 'HTML' },
        { key: 3, value: 'CSS' },
        { key: 4, value: 'JS' },
    ]

    const levels = [
        { key: 'beginner', value: 'Başlangıç' },
        { key: 'jr_developer', value: 'Jr. Developer' },
        { key: 'sr_developer', value: 'Sr. Developer' }
    ]

    const [level, setLevel] = useState('beginner');
    const [avatar, setAvatar] = useState(false);
    const [image, setImage] = useState(false);
    const [name, setName] = useState();
    const [desc, setDesc] = useState();
    const [rule, setRule] = useState(true);
    const [rules, setRules] = useState([
        { key: 1, value: '1. kuralı kabul edilyorum', checked: false },
        { key: 2, value: '2. kuralı kabul edilyorum', checked: false },
        { key: 3, value: '3. kuralı kabul edilyorum', checked: false }
    ]);
    const [gender, setGender] = useState('1');
    const [categories, setCategories] = useState([2, 4]);

    const selectedGender = genders.find(g => g.key === gender);
    const selectedLevel = levels.find(g => g.key === level);
    const selectedCategories = categories && categoryList.filter(c => categories.includes(c.key))

    const multipleCheckboxButtonDisabled = rules.every(rule => rule.checked);

    useEffect(() => {
        if (avatar) {
            const fileReader = new FileReader();
            fileReader.addEventListener('load', function () {
                setImage(this.result)
            })
            fileReader.readAsDataURL(avatar);
        }
    }, [avatar])

    const checkRule = (key, checked) => {
        setRules(rules => rules.map(rule => {

            if (key === rule.key) {
                rule.checked = checked
            }
            return rule;
        }))
    }

    const submitPng = () => {
      const formData = new FormData();
      formData.append('avatar', avatar);
      formData.append('name', name)

      fetch('url', {
        method: 'POST',
        body: formData
      })
    }

    return (
        <>
            <Card>
                <Header text="File Input" />

                <label>
                    <input type="file" onChange={e => setAvatar(e.target.files[0])} />
                </label>
                <br />

                <>
                    {image && <img src={image} />}
                </>

                <Button text="Resmi Gönder" onClick={submitPng} />

            </Card>
            <Card>
                <Header text="Options & Checkbox" />

                {levels.map((l, index) => (
                    <label className="mr-2" key={index}>
                        <input type="radio" value={l.key} checked={l.key === level} onChange={e => setLevel(e.target.value)} />
                        {l.value}
                    </label>
                ))}

                <pre>{JSON.stringify(selectedLevel, null, 2)}</pre>
            </Card>
            <Card>
                <Header text="Multiple Checkbox" />
                {rules.map(rule => (
                    <label key={rule.key}>
                        <input type="checkbox" checked={rule.checked} onChange={e => checkRule(rule.key, e.target.checked)} />
                        {rule.value}
                        <br />
                    </label>
                ))}

                <pre>{JSON.stringify(rules, null, 2)}</pre>

                <Button text="Devam et" disabled={!multipleCheckboxButtonDisabled} />
            </Card>
            <Card>
                <Header text="Checkbox" />
                <label>
                    <input type="checkbox" checked={rule} onChange={e => setRule(e.target.checked)} />
                    Kuralları kabul ediyorum.
                </label>

                <Button text="Devam et" disabled={!rules} />
            </Card>

            <Card>
                <Header text="Multiple Select" />
                <Button text="Kategorileri Seç" onClick={() => setCategories([1, 2, 3, 4])} />
                <select className="bg-slate-100 p-2" multiple={true} value={categories} onChange={e => setCategories([...e.target.selectedOptions].map(option => +option.value))}>
                    {categoryList.map(category => (<option value={category.key} key={category.key}>{category.value}</option>))}
                </select><br />
                <span className="bg-green-200"><pre>{JSON.stringify(selectedCategories, null, 2)}</pre></span>
            </Card>

            <Card>

                <Header text="Select" />
                <select className="bg-slate-300 p-2" value={gender} onChange={e => setGender(e.target.value)}>
                    <option value="">Seçin</option>
                    {genders.map(gender => (<option value={gender.key} key={gender.key}>{gender.value}</option>))}
                </select><br />
                <span className="bg-green-200">{gender}</span><br />
                <span className="bg-green-200"><pre>{JSON.stringify(selectedGender, null, 2)}</pre></span>
            </Card>

            <Card>
                <Header text="Textarea" />
                <textarea className="bg-slate-300 p-2" value={desc} onChange={e => setDesc(e.target.value)} /><br />
                <span className="bg-green-200">{desc}</span>
            </Card>

            <Card>
                <Header text="Input" />
                <Button onClick={() => setName('ahmet')} text="İsmi Ahmet Yap" />
                <input className="bg-slate-300 p-2" type="text" value={name} onChange={e => setName(e.target.value)} /><br /><br />
                <span className="bg-green-200">{name}</span>
            </Card>
        </>
    );
}

export default Lesson10;