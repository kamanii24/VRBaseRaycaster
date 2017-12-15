# VRBaseRaycaster v1.0.0
VRでよく使う視点選択機能を使いやすくまとめたスクリプトです。
![img000](https://i.imgur.com/cfqQ6iL.gif)<br>

# 使い方
#### 初期設定
VRBaseRaycaster/Prefabs/VRBaseRaycasterをシーンへ追加します。<br>
追加したVRBaseRaycasterのInspector中の**PlayerHead**へ視線の起点となるオブジェクトを追加します。<br>
**TargetLayer**へRaycastの判定を取る対象のLayerを設定します。<br>

![img001](https://i.imgur.com/MIwTdTj.png)<br>

#### 注視対象の設定<br>
注視対象にしたいオブジェクトにはColliderと、**VRBaseEventTrigger**コンポーネントを追加します。<br>
VRBaseEventTriggerには4つのイベントハンドラが用意されています。<br>
- OnEnter<br>
視点が対象物と衝突した時に呼び出されます。<br>

- OnHover<br>
視点が対象物と衝突しつつけている間呼び出されます。<br>

- OnExit<br>
視点が対象物から外れた時に呼び出されます。<br>

- OnProcess<br>
対象物を注視し続け、**VRBaseRaycast**の**Fixed Process Time**に設定された時間経過した場合に呼び出されます。<br>
視点による選択を実行する場合に使用します。<br>

![img002](https://i.imgur.com/rNcXiKS.png)<br>

## ビルド環境<br>
Unity 2017.2.0p4<br>
macOS High Sierra 10.13.2

